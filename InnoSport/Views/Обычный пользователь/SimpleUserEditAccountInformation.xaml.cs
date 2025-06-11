using InnoSport.Data;
using InnoSport.Models;
using System.Windows;

namespace InnoSport.Views.Обычный_пользователь
{
    public partial class SimpleUserEditAccountInformation : Window
    {
        private int userId;

        public SimpleUserEditAccountInformation(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            LoadUserData();
        }

        private void LoadUserData()
        {
            using var db = new AppDBContext();
            var user = db.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                SurnameTextBox.Text = user.Surname;
                NameTextBox.Text = user.Name;
                OtchestvoTextBox.Text = user.Otchestvo ?? "";
                PhoneNumberTextBox.Text = user.PhoneNumber;
                EmailTextBox.Text = user.Email ?? "";
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SurnameTextBox.Text) ||
                string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                string.IsNullOrWhiteSpace(PhoneNumberTextBox.Text))
            {
                MessageBox.Show("Поля Фамилия, Имя и Телефон обязательны для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using var db = new AppDBContext();
            var user = db.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                MessageBox.Show("Пользователь не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            user.Surname = SurnameTextBox.Text.Trim();
            user.Name = NameTextBox.Text.Trim();
            user.Otchestvo = OtchestvoTextBox.Text.Trim();
            user.PhoneNumber = PhoneNumberTextBox.Text.Trim();
            user.Email = EmailTextBox.Text.Trim();

            db.SaveChanges();

            MessageBox.Show("Данные успешно сохранены.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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