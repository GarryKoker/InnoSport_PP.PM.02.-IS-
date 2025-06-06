using System;
using System.Linq;
using System.Windows;
using InnoSport.Data;
using InnoSport.Models;
using InnoSport.Helpers;

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
            string login = LoginTextBox.Text.Trim();
            string name = NameTextBox.Text.Trim();
            string surname = SurnameTextBox.Text.Trim();
            string phoneNumber = PhoneNumberTextBox.Text.Trim();
            string password = PasswordTextBox.Password.Trim();
            string secondPassword = SecondPasswordTextBox.Password.Trim();

            if (string.IsNullOrEmpty(login) ||
                string.IsNullOrEmpty(name) ||
                string.IsNullOrEmpty(surname) ||
                string.IsNullOrEmpty(phoneNumber) ||
                string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(secondPassword))
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (password != secondPassword)
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var db = new AppDBContext())
                {
                    if (db.Users.Any(u => u.Login == login))
                    {
                        MessageBox.Show("Пользователь с таким логином уже существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (db.Users.Any(u => u.PhoneNumber == phoneNumber))
                    {
                        MessageBox.Show("Пользователь с таким номером телефона уже существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    var user = new User
                    {
                        Login = login,
                        Name = name,
                        Surname = surname,
                        PhoneNumber = phoneNumber,
                        Password = PasswordHelper.HashPassword(password),
                        Role = (int)Roles.User // По умолчанию обычный пользователь
                    };
                    db.Users.Add(user);
                    db.SaveChanges();

                    MessageBox.Show("Регистрация прошла успешно", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    new LoginWindow().Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка регистрации: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void GoToLoginWindowHyperlink_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            this.Close();
        }
    }
}