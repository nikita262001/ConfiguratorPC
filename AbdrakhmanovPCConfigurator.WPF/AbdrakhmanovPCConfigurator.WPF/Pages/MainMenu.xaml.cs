using System.Windows;
using System.Windows.Controls;

namespace AbdrakhmanovPCConfigurator.WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();

            var person = GetItemObject.AuthrizationPerson;
            if (person.Type == "Администратор")
                GetItemObject.MainTitle.Text = $"Основное меню, Администратор: {person.Surname} {person.Name} {person.Patronymic}";
            else
            {
                buttonComponents.Visibility = Visibility.Collapsed;
                GetItemObject.MainTitle.Text = $"Основное меню, Клиент: {person.Surname} {person.Name} {person.Patronymic}";
            }
            GetItemObject.FrameMenu = frameMenu;
        }

        private void FullComponents_Click(object sender, RoutedEventArgs e) =>
            frameMenu.Content = new FullConfiguratorPC();


        private void Components_Click(object sender, RoutedEventArgs e) =>
            frameMenu.Content = new ComponentsPage();


        private void ConfiguratorPC_Click(object sender, RoutedEventArgs e) =>
            frameMenu.Content = new ConfiguratorPC();


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            GetItemObject.AuthrizationPerson = null;
            GetItemObject.FrameMain.Content = new Authorization();
        }
    }
}
