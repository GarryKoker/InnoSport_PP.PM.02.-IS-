using System.Windows;
using InnoSport.Models;
using InnoSport.Data;

namespace InnoSport.Views.Администратор
{
    public partial class AdministratorEditInformationWindow : Window
    {
        private User admin;

        public AdministratorEditInformationWindow(User admin)
        {
            InitializeComponent();
            this.admin = admin;

            SurnameTextBox.Text = admin.Surname;
            NameTextBox.Text = admin.Name;
            OtchestvoTextBox.Text = admin.Otchestvo ?? "";
            PhoneNumberTextBox.Text = admin.PhoneNumber;
            EmailTextBox.Text = admin.Email ?? "";
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SurnameTextBox.Text) ||
                string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                string.IsNullOrWhiteSpace(PhoneNumberTextBox.Text))
            {
                MessageBox.Show("Поля Фамилия, Имя и Телефон обязательны.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using var db = new AppDBContext();
            var dbAdmin = db.Users.FirstOrDefault(u => u.Id == admin.Id);
            if (dbAdmin == null)
            {
                MessageBox.Show("Пользователь не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dbAdmin.Surname = SurnameTextBox.Text.Trim();
            dbAdmin.Name = NameTextBox.Text.Trim();
            dbAdmin.Otchestvo = OtchestvoTextBox.Text.Trim();
            dbAdmin.PhoneNumber = PhoneNumberTextBox.Text.Trim();
            dbAdmin.Email = EmailTextBox.Text.Trim();

            db.SaveChanges();

            admin.Surname = dbAdmin.Surname;
            admin.Name = dbAdmin.Name;
            admin.Otchestvo = dbAdmin.Otchestvo;
            admin.PhoneNumber = dbAdmin.PhoneNumber;
            admin.Email = dbAdmin.Email;

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