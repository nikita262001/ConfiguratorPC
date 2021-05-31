using AbdrakhmanovPCConfigurator.WPF.Classes;
using AbdrakhmanovPCConfigurator.WPF.Properties;
using AbdrakhmanovPCConfigurator.WPF.Windows;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AbdrakhmanovPCConfigurator.WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для ConfiguratorPC.xaml
    /// </summary>
    public partial class ConfiguratorPC : Page
    {
        List<Component> componentsTree = new List<Component>();
        List<ListView> listViewObjects = new List<ListView>();
        AbdrakhmanovAPCConfiguratorEntities db = GetItemObject.DB;
        public ConfiguratorPC()
        {
            InitializeComponent();

            if (GetItemObject.AuthrizationPerson.Type == "Клиент")
                buttonBuy.Visibility = Visibility.Visible;

            foreach (var item in GetItemObject.DB.Components.Where(obj => obj.Parent_Id == null && obj.Parametrs == null))
                AddParametrInMainStack(item);
        }

        private void AddParametrInMainStack(Component component, Component componentSelected = null)
        {
            TextBlock text = new TextBlock { Width = 300, Text = component.Name + ": ", };
            var components = db.Components.Where(obj => obj.Parent_Id == component.Id).ToList();
            var componentNoSelect = new Component { Name = "Не выбранно" };
            components.Add(componentNoSelect);


            Style styleCombo = Resources["ComboBoxFullName"] as Style;
            ComboBox combo = new ComboBox { Width = 300, ItemsSource = components.OrderByDescending(obj => obj.Name == "Не выбранно"), Style = styleCombo };
            combo.SelectedItem = componentNoSelect;
            combo.SelectionChanged += Combo_SelectionChanged;

            StackPanel stack = new StackPanel { Orientation = Orientation.Horizontal, };
            stack.Children.Add(text);
            stack.Children.Add(combo);

            Style styleStack = Resources["ListViewGetItems"] as Style;
            ListView listObjects = new ListView { Tag = combo, Style = styleStack, Visibility = Visibility.Collapsed };
            listObjects.SelectionChanged += ListObjects_SelectionChanged;

            listViewObjects.Add(listObjects);

            StackPanel stackMain = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 5, 0, 5),
            };
            stackMain.Children.Add(stack);
            stackMain.Children.Add(listObjects);

            mainStack.Children.Add(stackMain);

            if (componentSelected != null)
                combo.SelectedItem = componentSelected;
        }

        private void ListObjects_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            txtPrice.Text = $"{GetPriceSelectedComponents()}р.";


        private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            var listView = listViewObjects.FirstOrDefault(obj => obj.Tag == combo);
            var component = combo.SelectedItem as Component;

            if (component.Name == "Не выбранно")
                listView.Visibility = Visibility.Collapsed;
            else
            {
                AddAllComponentsTree(component);
                listView.ItemsSource = componentsTree;
                listView.Visibility = Visibility.Visible;
            }
        }

        private void InformationAboutComponent_Click(object sender, RoutedEventArgs e)
        {
            InformationWindow window = new InformationWindow(((Button)sender).DataContext as Component);
            window.ShowDialog();
        }

        private void AddComponentsTree(Component component)
        {
            var components = db.Components.Where(obj => obj.Parent_Id == component.Id).ToList();
            if (components.Count != 0)
            {
                for (int i = 0; i < components.Count; i++)
                    AddComponentsTree(components[i]);
            }
            else
                componentsTree.Add(component);
        }

        private void AddAllComponentsTree(Component component)
        {
            componentsTree = new List<Component>();
            AddComponentsTree(component);
        }

        private void SaveConfiguratorPC_Click(object sender, RoutedEventArgs e)
        {
            if (txtNameFinishedPC.Text.Length != 0)
            {
                List<int> listComponents = GetIdSelectedComponents();
                FinishedPC finished = new FinishedPC { SetComponents = listComponents, NameFinishedPC = txtNameFinishedPC.Text };
                GetItemObject.DB.FinishedPCs.Add(finished);
                GetItemObject.DB.SaveChanges();
                GetItemObject.FrameMenu.Content = null;
                MessageBox.Show("Сборка компьютера была успешно сохранена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Введите наименование сборки", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private int GetPriceSelectedComponents()
        {
            List<Component> components = GetSelectedComponents();
            int price = 0;

            foreach (var item in components)
                price += item.GetPriceComponent;

            return price;
        }

        private List<int> GetIdSelectedComponents()
        {
            List<Component> components = GetSelectedComponents();
            List<int> list = new List<int>();

            foreach (var item in components)
                list.Add(item.Id);

            return list;
        }

        private List<Component> GetSelectedComponents()
        {
            List<Component> list = new List<Component>();

            foreach (var item in listViewObjects)
            {
                var componentSelect = (item.SelectedItem as Component);
                if (componentSelect != null)
                    if (componentSelect.Name != "Не выбранно")
                        list.Add(item.SelectedItem as Component);
            }

            return list;
        }

        private void BuyConfiguratorPC_Click(object sender, RoutedEventArgs e)
        {
            VoltageConvergenceCheck();
            var answer = MessageBox.Show("Вы уверены что хотите приобрести данную сборку ПК?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (answer == MessageBoxResult.Yes)
                MessageBox.Show($"Через 3 дня вам првезут данную сборку к вашему дому по адресу: {GetItemObject.AuthrizationPerson.Address}\n" +
                    "Если это не ваш адресс то свяжитесь с службой поддержки.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void VoltageConvergenceCheck()
        {
            int wattHave = 0;
            int wattnecessary = 0;

            foreach (var component in GetSelectedComponents())
            {
                foreach (var parametr in component.GetPrametrsComponent)
                {
                    if (parametr.Key == "Ватт")
                    {
                        try
                        {
                            if (component.GetRootParentComponent.Name == "Блок питания")
                                wattHave += int.Parse(parametr.Value);
                            else
                                wattnecessary += int.Parse(parametr.Value);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }


            if (wattHave == 0)
                MessageBox.Show("В данной сборке отсутствует блок питания", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (wattHave - wattnecessary >= 0)
                MessageBox.Show("В данной сборке проблем с напряжение не должно возникнуть", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (wattHave - wattnecessary < 0)
                MessageBox.Show("В данной сборке будут проблемы с напряжением", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
