using AbdrakhmanovPCConfigurator.WPF.Windows;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для FullConfiguratorPC.xaml
    /// </summary>
    public partial class FullConfiguratorPC : Page
    {
        public FullConfiguratorPC()
        {
            InitializeComponent();
            list.ItemsSource = GetItemObject.DB.FinishedPCs.ToList();
        }

        private void InformationAboutFinishedPC_Click(object sender, RoutedEventArgs e)
        {
            InformationWindow window = new InformationWindow(((Button)sender).DataContext as FinishedPC);
            window.ShowDialog();
        }

        private void RemoveThisFinishedPC_Click(object sender, RoutedEventArgs e)
        {
            var answer = MessageBox.Show("Вы уверены что хотите удалить данную сборку ПК?","Уведомление", MessageBoxButton.YesNo,MessageBoxImage.Information);
            if (answer == MessageBoxResult.Yes)
            {
                var finishedPC = ((Button)sender).DataContext as FinishedPC;
                GetItemObject.DB.FinishedPCs.Remove(finishedPC);
                GetItemObject.DB.SaveChanges();
                MessageBox.Show("Выбранная сборка была успешно удалена.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

                list.ItemsSource = GetItemObject.DB.FinishedPCs.ToList();
            }
        }
        private void BuyConfiguratorPC_Click(object sender, RoutedEventArgs e)
        {
            var finishedPC = ((Button)sender).DataContext as FinishedPC;
            VoltageConvergenceCheck(finishedPC.GetListComponents);
            var answer = MessageBox.Show("Вы уверены что хотите приобрести данную сборку ПК?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (answer == MessageBoxResult.Yes)
                MessageBox.Show($"Через 3 дня вам првезут данную сборку к вашему дому по адресу: {GetItemObject.AuthrizationPerson.Address}\n" +
                    "Если это не ваш адресс то свяжитесь с службой поддержки.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void VoltageConvergenceCheck(List<Component> components)
        {
            int wattHave = 0;
            int wattnecessary = 0;

            foreach (var component in components)
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
