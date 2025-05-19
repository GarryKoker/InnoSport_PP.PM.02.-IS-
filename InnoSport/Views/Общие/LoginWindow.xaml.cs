using InnoSport.Data;
using InnoSport.Views.Обычный_пользователь;
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
using System.Windows.Shapes;

namespace InnoSport
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        public void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string Login = LoginTextBox.Text;
            string Password = PasswordTextBox.Text;

            if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Все поля должны быть заполнены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var db = new AppDBContext())
            {
                if (db.Users.Any(u => u.Login == Login && u.Password == Password))
                {
                    var User = db.Users.FirstOrDefault(u => u.Login == Login);
                    switch (User.Role)
                    {
                        case 0:
                            SimpleUserMainWindow NewWindow = new SimpleUserMainWindow(User);
                            break;
                    }
                    SimpleUserMainWindow window = new SimpleUserMainWindow(db.Users.FirstOrDefault(u => u.Login == Login));
                    window.Show();
                    this.Close();
                }

                else
                {
                    MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void GoToRegistrationHyperlink_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            this.Close();
        }
    }
}
