using AbdrakhmanovPCConfigurator.WPF.Classes;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AbdrakhmanovPCConfigurator.WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddComponent.xaml
    /// </summary>
    public partial class ComponentsPage : Page
    {
        byte[] imageInByte = null;
        AbdrakhmanovAPCConfiguratorEntities db = GetItemObject.DB;
        Component lastComponent = null;
        public ComponentsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            comboTypeComponent.ItemsSource = db.Components.Where(obj => obj.Parametrs == null && obj.Parent_Id == null).ToList();
        }

        private void MainComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            stackTree.Children.Clear();

            int id = (comboTypeComponent.SelectedItem as Component).Id;
            AddChildrenInStack(id);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboChanged = sender as ComboBox;
            var component = (comboChanged.SelectedItem as Component);
            CleaningAfterStack(int.Parse(comboChanged.Tag.ToString()));
            if (component != null)
            {
                if (component.Name != "Не выбранно")
                {
                    AddChildrenInStack(component.Id);
                    lastComponent = component;
                    buttonAddParametrs.Visibility = Visibility.Visible;
                    stackFullParametrs.Visibility = Visibility.Collapsed;
                }
                else if (component.Name == "Не выбранно" && stackTree.Children.Count == 1)
                {
                    buttonAddParametrs.Visibility = Visibility.Collapsed;
                    stackFullParametrs.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void AddChildrenInStack(int idComponent)
        {
            var components = db.Components.Where(obj => obj.Parent_Id == idComponent).ToList();
            components.Add(new Component { Name = "Не выбранно", Parent_Id = idComponent });
            ComboBox combo = new ComboBox
            {
                ItemsSource = components.OrderBy(obj => obj.Name != "Не выбранно"),
                DisplayMemberPath = "Name",
                Width = 200,
                Margin = new Thickness(5, 0, 5, 0),
                Tag = idComponent,
            };
            combo.SelectionChanged += ComboBox_SelectionChanged;

            Button buttonRemove = new Button
            {
                Content = "-",
                Width = 25,
                Height = 25,
                Padding = new Thickness(3),
                Margin = new Thickness(5, 0, 5, 0),
                Tag = idComponent,
            };
            buttonRemove.Click += ButtonRemove_Click;

            TextBox textBox = new TextBox
            {
                Width = 200,
                Margin = new Thickness(5, 0, 5, 0),
                Tag = idComponent,
            };

            Button buttonAdd = new Button
            {
                Content = "+",
                Width = 25,
                Height = 25,
                Padding = new Thickness(3),
                Margin = new Thickness(5, 0, 5, 0),
                Tag = idComponent,
            };
            buttonAdd.Click += ButtonAdd_Click;


            StackPanel stack = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 5, 0, 5),
                Tag = idComponent,
            };
            stack.Children.Add(combo);
            stack.Children.Add(buttonRemove);
            stack.Children.Add(textBox);
            stack.Children.Add(buttonAdd);

            stackTree.Children.Add(stack);
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            var senderTag = (sender as Button).Tag.ToString();
            var stack = FindStack(senderTag);
            var comboBox = FindComboBoxInStack(stack);
            var textBox = FindTextBoxInStack(stack);
            var component = comboBox.SelectedItem as Component;
            if (component != null)
            {
                if (component.Name != "Не выбранно")
                {
                    component.RemoveKascadeComponent();
                    db.SaveChanges();
                    MessageBox.Show("Удаление данных прошло успешно.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    UpdateDataInStack(textBox, comboBox, int.Parse((stack.Tag.ToString())));
                }
            }
            else
                MessageBox.Show("Вы не выбрали объект который нужно удалить", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AddParametrInStack()
        {
            TextBlock textKey = new TextBlock { HorizontalAlignment = HorizontalAlignment.Right, Text = "Наименование" };
            TextBox textBoxKey = new TextBox { Width = 200, Margin = new Thickness(5, 0, 5, 0), Tag = "Key" };

            TextBlock textValue = new TextBlock { HorizontalAlignment = HorizontalAlignment.Right, Text = "Значение" };
            TextBox textBoxValue = new TextBox { Width = 200, Margin = new Thickness(5, 0, 5, 0), Tag = "Value" };

            Button buttonRemove = new Button { Content = "-", Width = 25, Height = 25, Padding = new Thickness(3), Margin = new Thickness(5, 0, 5, 0) };
            buttonRemove.Click += ButtonRemoveParametr_Click;

            StackPanel stack = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 5, 0, 5),
                Tag = buttonRemove,
            };
            stack.Children.Add(textKey);
            stack.Children.Add(textBoxKey);
            stack.Children.Add(textValue);
            stack.Children.Add(textBoxValue);
            stack.Children.Add(buttonRemove);

            stackParametrs.Children.Add(stack);
        }

        private void ButtonRemoveParametr_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var children = stackParametrs.Children;
            for (int i = 0; i < children.Count; i++)
            {
                var stack = children[i] as StackPanel;
                if (stack.Tag == button)
                {
                    stackParametrs.Children.Remove(stack);
                    break;
                }
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var senderTag = (sender as Button).Tag.ToString();
            var stack = FindStack(senderTag);
            var comboBox = FindComboBoxInStack(stack);
            var textBox = FindTextBoxInStack(stack);
            if (textBox.Text.Length != 0)
            {
                var idParent = int.Parse(senderTag);
                db.Components.Add(new Component { Name = textBox.Text, Parent_Id = idParent });
                db.SaveChanges();
                MessageBox.Show("Добавление данных прошло успешно.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                UpdateDataInStack(textBox, comboBox, idParent);
            }
            else
                MessageBox.Show("Вы не ввели текст для добавления.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void UpdateDataInStack(TextBox textBox, ComboBox comboBox, int idParent)
        {
            textBox.Text = "";
            comboBox.ItemsSource = null;
            var components = db.Components.Where(obj => idParent == obj.Parent_Id).ToList();
            components.Add(new Component { Name = "Не выбранно", Parent_Id = idParent });
            comboBox.ItemsSource = components.OrderBy(obj => obj.Name != "Не выбранно");
            if (stackTree.Children.Count == 1)
            {
                buttonAddParametrs.Visibility = Visibility.Collapsed;
                stackFullParametrs.Visibility = Visibility.Collapsed;
            }
        }

        private void CleaningAfterStack(int idParent)
        {
            for (int i = stackTree.Children.Count - 1; i >= 0; i--)
            {
                var stack = stackTree.Children[i] as StackPanel;
                if (idParent == int.Parse((stack.Tag.ToString())))
                    break;
                else
                    stackTree.Children.Remove(stack);
            }
        }

        private StackPanel FindStack(string senderTag)
        {
            foreach (var item in stackTree.Children)
            {
                var stack = item as StackPanel;
                if (int.Parse(stack.Tag.ToString()) == int.Parse(senderTag))
                    return stack;
            }
            return null;
        }

        private ComboBox FindComboBoxInStack(StackPanel stack)
        {
            foreach (var item in stack.Children)
            {
                var combo = item as ComboBox;
                if (combo != null)
                    return combo;
            }
            return null;
        }

        private TextBox FindTextBoxInStack(StackPanel stack)
        {
            foreach (var item in stack.Children)
            {
                var combo = item as TextBox;
                if (combo != null)
                    return combo;
            }
            return null;
        }

        private void AddParametrs_Click(object sender, RoutedEventArgs e)
        {
            buttonAddParametrs.Visibility = Visibility.Collapsed;
            stackFullParametrs.Visibility = Visibility.Visible;
            AddParametrInStack();
        }

        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog().GetValueOrDefault())
            {
                image.Source = new BitmapImage(new Uri(openFile.FileName));
                image.Visibility = Visibility.Visible;
                imageInByte = File.ReadAllBytes(openFile.FileName);
            }
        }

        private void AddParametr_Click(object sender, RoutedEventArgs e) =>
            AddParametrInStack();

        private void SaveWithParametrs_Click(object sender, RoutedEventArgs e)
        {
            List<Parametr> parametrs = new List<Parametr>();
            var stackParametrsChild = stackParametrs.Children;
            for (int i = 0; i < stackParametrsChild.Count; i++)
            {
                string key = "";
                string value = "";
                var stackChild = (stackParametrsChild[i] as StackPanel).Children;
                for (int f = 0; f < stackChild.Count; f++)
                {
                    var textBox = stackChild[f] as TextBox;
                    if (textBox != null)
                    {
                        if (textBox.Tag.ToString() == "Key")
                            key = textBox.Text;
                        else if(textBox.Tag.ToString() == "Value")
                            value = textBox.Text;
                    }
                }
                if (key != "" && value != "")
                {
                    parametrs.Add(new Parametr { Key = key, Value = value, imageInByte = null });
                }
            }

            if (textPrice.Text.Length != 0)
            {
                parametrs.Add(new Parametr { Key = "Price", Value = textPrice.Text, imageInByte = null });
                parametrs.Add(new Parametr { Key = "Image", Value = "", imageInByte = imageInByte });

                Component component = new Component
                {
                    Name = "",
                    Parent_Id = lastComponent.Id,
                    Parametrs = JsonConvert.SerializeObject(parametrs),
                };
                GetItemObject.DB.Components.Add(component);
                GetItemObject.DB.SaveChanges();
                GetItemObject.FrameMenu.Content = null;
                MessageBox.Show("Данны успешно сохранены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Вы не ввели наименование компонента","Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) =>
            e.Handled = !new Regex("[0-9]").IsMatch(e.Text);
    }
}
