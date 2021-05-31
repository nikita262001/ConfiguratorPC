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
    /// Логика взаимодействия для RegistrationPerson.xaml
    /// </summary>
    public partial class RegistrationPerson : Page
    {
        public RegistrationPerson()
        {
            InitializeComponent();
            GetItemObject.MainTitle.Text = "Меню регистрации клиента";
        }

        private void Back_Click(object sender, RoutedEventArgs e) =>
            GetItemObject.FrameMain.Content = new Authorization();

        private void CreatePerson_Click(object sender, RoutedEventArgs e)
        {
            if (txtAddress.Text.Length != 0 && txtLogin.Text.Length != 0 && txtName.Text.Length != 0 &&
                txtNumberPhone.Text.Length != 0 && txtPassword.Text.Length != 0 && txtPatronymic.Text.Length != 0 &&
                txtSurname.Text.Length != 0)
            {
                var answerNewLogin = GetItemObject.DB.People.Where(obj => obj.Login == txtLogin.Text).FirstOrDefault();
                if (answerNewLogin == null)
                {
                    var person = new Person
                    {
                        Address = txtAddress.Text,
                        Surname = txtSurname.Text,
                        Name = txtName.Text,
                        Patronymic = txtPatronymic.Text,
                        Login = txtLogin.Text,
                        Password = txtPassword.Text,
                        NumberPhone = txtNumberPhone.Text,
                        Type = "Клиент",
                    };
                    GetItemObject.DB.People.Add(person);
                    GetItemObject.DB.SaveChanges();
                    MessageBox.Show("Вы успешно зарегистрированны", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    GetItemObject.FrameMain.Content = new Authorization();
                }
                else
                    MessageBox.Show("Такой логин уже существует", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
                MessageBox.Show("Не все данные были введены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
