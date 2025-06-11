using System.Windows;
using InnoSport.Models;
using InnoSport.Data;

namespace InnoSport.Views.Главный_администратор
{
    public partial class ChiefAdministratorAddUserWindow : Window
    {
        public ChiefAdministratorAddUserWindow()
        {
            InitializeComponent();
            RoleComboBox.ItemsSource = System.Enum.GetValues(typeof(Roles));
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SurnameTextBox.Text) ||
                string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                string.IsNullOrWhiteSpace(PhoneNumberTextBox.Text) ||
                string.IsNullOrWhiteSpace(LoginTextBox.Text) ||
                string.IsNullOrWhiteSpace(PasswordTextBox.Text) ||
                RoleComboBox.SelectedItem == null)
            {
                MessageBox.Show("Заполните все обязательные поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using var db = new AppDBContext();
            if (db.Users.Any(u => u.Login == LoginTextBox.Text.Trim()))
            {
                MessageBox.Show("Пользователь с таким логином уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var user = new User
            {
                Surname = SurnameTextBox.Text.Trim(),
                Name = NameTextBox.Text.Trim(),
                PhoneNumber = PhoneNumberTextBox.Text.Trim(),
                Login = LoginTextBox.Text.Trim(),
                Password = PasswordTextBox.Text.Trim(),
                Role = (int)(Roles)RoleComboBox.SelectedItem
            };
            db.Users.Add(user);
            db.SaveChanges();

            MessageBox.Show("Пользователь успешно добавлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            this.DialogResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}