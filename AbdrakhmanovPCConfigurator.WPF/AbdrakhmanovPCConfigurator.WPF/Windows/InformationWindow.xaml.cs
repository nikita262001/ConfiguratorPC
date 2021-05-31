using AbdrakhmanovPCConfigurator.WPF.Classes;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace AbdrakhmanovPCConfigurator.WPF.Windows
{
    /// <summary>
    /// Логика взаимодействия для InformationAboutComponent.xaml
    /// </summary>
    public partial class InformationWindow : Window
    {
        public InformationWindow(Component component)
        {
            InitializeComponent();
            image.Visibility = Visibility.Visible;
            txtName.Text = component.GetFullName;
            InformationAboutComponent(component);
        }

        private void InformationAboutComponent(Component component)
        {
            StackPanel stackPanel = new StackPanel { HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(0, 10, 0, 0) };
            stackMain.Children.Add(stackPanel);
            List<Parametr> parametrs = component.GetPrametrsComponent;
            foreach (var item in parametrs)
            {
                if (item.Key == "Image" && item.imageInByte != null)
                    image.Source = item.GetBitmapSource();
                else if (item.Key != "Image")
                    AddInStackData(item, stackPanel);
            }
        }

        private void AddInStackData(Parametr parametr, StackPanel stackPanel)
        {
            TextBlock textBlockKey = new TextBlock { Text = parametr.Key + ": ", TextAlignment = TextAlignment.Right, FontSize = 20 };
            TextBlock textBlockValue = new TextBlock { Text = parametr.Value, TextAlignment = TextAlignment.Left, FontSize = 20 };

            StackPanel stack = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                Orientation = Orientation.Horizontal
            };
            stack.Children.Add(textBlockKey);
            stack.Children.Add(textBlockValue);

            stackPanel.Children.Add(stack);
        }

        public InformationWindow(FinishedPC finishedPC)
        {
            InitializeComponent();
            txtName.Text = finishedPC.NameFinishedPC;
            var components = finishedPC.GetListComponents;
            InformationAboutFinishedPC(components);
            var countNoFind = finishedPC.GetListComponentsId.Count - components.Count;
            if (countNoFind != 0)
                MessageBox.Show($"В данной сборке не удалось найти {countNoFind} компонентов", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void InformationAboutFinishedPC(List<Component> components)
        {
            Style styleStack = Resources["ListViewGetItems"] as Style;
            ListView listObjects = new ListView { Style = styleStack, ItemsSource = components };
            stackMain.Children.Add(listObjects);
        }

        private void InformationAboutComponent_Click(object sender, RoutedEventArgs e)
        {
            InformationWindow window = new InformationWindow(((Button)sender).DataContext as Component);
            window.ShowDialog();
        }
    }
}
