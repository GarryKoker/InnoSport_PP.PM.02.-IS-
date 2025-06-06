using System;
using System.Linq;
using System.Windows;
using InnoSport.Data;
using InnoSport.Models;
using InnoSport.Helpers;
using InnoSport.Views.Обычный_пользователь;
using InnoSport.Views.Спортсмен;
using InnoSport.Views.Тренер;
using InnoSport.Views.Администратор;
using InnoSport.Views.Главный_администратор;

namespace InnoSport
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string loginOrPhone = LoginTextBox.Text.Trim();
            string password = PasswordTextBox.Password.Trim();

            if (string.IsNullOrEmpty(loginOrPhone) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин/телефон и пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var db = new AppDBContext())
                {
                    string passwordHash = PasswordHelper.HashPassword(password);
                    var user = db.Users.FirstOrDefault(u =>
                        (u.Login == loginOrPhone || u.PhoneNumber == loginOrPhone) &&
                        u.Password == passwordHash);

                    if (user == null)
                    {
                        MessageBox.Show("Неверный логин/телефон или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    switch ((Roles)user.Role)
                    {
                        case Roles.User:
                            new SimpleUserMainWindow(user).Show();
                            break;
                        case Roles.Sportsman:
                            new SportsmenMainWindow(user).Show();
                            break;
                        case Roles.Trainer:
                            new TrainerMainWindow(user).Show();
                            break;
                        case Roles.Administrator:
                            new AdministratorMainWindow(user).Show();
                            break;
                        case Roles.ChiefAdministrator:
                            new InnoSport.Views.Главный_администратор.ChiefAdministratorMainWindow().Show();
                            break;
                        default:
                            MessageBox.Show("Неизвестная роль пользователя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                    }

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка входа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GoToRegistrationHyperlink_Click(object sender, RoutedEventArgs e)
        {
            new RegistrationWindow().Show();
            this.Close();
        }
    }
}