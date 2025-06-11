using System.Windows;
using InnoSport.Models;
using InnoSport.Data;

namespace InnoSport.Views.Администратор
{
    public partial class AdministratorEditUsersWindow : Window
    {
        private User user;

        public AdministratorEditUsersWindow(User user)
        {
            InitializeComponent();
            this.user = user;

            SurnameTextBox.Text = user.Surname;
            NameTextBox.Text = user.Name;
            OtchestvoTextBox.Text = user.Otchestvo ?? "";
            PhoneNumberTextBox.Text = user.PhoneNumber;
            EmailTextBox.Text = user.Email ?? "";
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
            var dbUser = db.Users.FirstOrDefault(u => u.Id == user.Id);
            if (dbUser == null)
            {
                MessageBox.Show("Пользователь не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dbUser.Surname = SurnameTextBox.Text.Trim();
            dbUser.Name = NameTextBox.Text.Trim();
            dbUser.Otchestvo = OtchestvoTextBox.Text.Trim();
            dbUser.PhoneNumber = PhoneNumberTextBox.Text.Trim();
            dbUser.Email = EmailTextBox.Text.Trim();

            db.SaveChanges();

            user.Surname = dbUser.Surname;
            user.Name = dbUser.Name;
            user.Otchestvo = dbUser.Otchestvo;
            user.PhoneNumber = dbUser.PhoneNumber;
            user.Email = dbUser.Email;

            MessageBox.Show("Данные пользователя успешно обновлены.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            this.DialogResult = true;
            this.Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите удалить пользователя?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                using var db = new AppDBContext();
                var dbUser = db.Users.FirstOrDefault(u => u.Id == user.Id);
                if (dbUser != null)
                {
                    db.Users.Remove(dbUser);
                    db.SaveChanges();
                }
                MessageBox.Show("Пользователь удалён.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}