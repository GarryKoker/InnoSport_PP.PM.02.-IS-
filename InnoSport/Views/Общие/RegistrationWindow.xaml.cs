using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using InnoSport.Data;
using InnoSport.Models;
using InnoSport.Views.Обычный_пользователь;

namespace InnoSport
{
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        public void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            string Login = LoginTextBox.Text;
            string Name = NameTextBox.Text;
            string Surname = SurnameTextBox.Text;
            string PhoneNumber = PhoneNumberTextBox.Text;
            string Password = PasswordTextBox.Text;
            string SecondPassword = SecondPasswordTextBox.Text;

            if (string.IsNullOrEmpty(Login)
                || string.IsNullOrEmpty(Name)
                || string.IsNullOrEmpty(Surname)
                || string.IsNullOrEmpty(PhoneNumber)
                || string.IsNullOrEmpty(Password)
                || string.IsNullOrEmpty(SecondPassword))
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Password != SecondPassword)
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var db = new AppDBContext())
                {
                    if (db.Users.Any(u => u.Login == Login || u.PhoneNumber == PhoneNumber))
                    {
                        MessageBox.Show("Пользователь с таким логином или номером телефона уже существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    else
                    {
                        User user = new User
                        {
                            Login = Login,
                            Name = Name,
                            Surname = Surname,
                            PhoneNumber = PhoneNumber,
                            Password = Password
                        };
                        db.Users.Add(user);
                        db.SaveChanges();
                        MessageBox.Show("Регистрация прошла успешно", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        SimpleUserMainWindow simpleUserMainWindow = new SimpleUserMainWindow(user);
                    }
                }
            }

            catch(Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}\n\n{ex.InnerException?.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void GoToLoginWindowHyperlink_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}