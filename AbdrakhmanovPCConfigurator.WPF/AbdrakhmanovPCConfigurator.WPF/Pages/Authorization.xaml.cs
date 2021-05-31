using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AbdrakhmanovPCConfigurator.WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для Authrization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        public Authorization()
        {
            InitializeComponent();
            GetItemObject.MainTitle.Text = "Меню авторизации";
        }

        private void RegistrationOpenPage_Click(object sender, RoutedEventArgs e)
        {
            GetItemObject.FrameMain.Content = new RegistrationPerson();
        }

        private void MainOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            var person = GetItemObject.DB.People.Where(obj => obj.Password == textPasPassword.Password && obj.Login == textBoxLogin.Text).FirstOrDefault();
            if (person != null)
            {
                GetItemObject.AuthrizationPerson = person;
                GetItemObject.FrameMain.Content = new MainMenu();
            }
            else
                MessageBox.Show("Данные были введены не правильно", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
