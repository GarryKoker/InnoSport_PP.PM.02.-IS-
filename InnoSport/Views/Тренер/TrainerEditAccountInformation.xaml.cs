using System.Windows;
using InnoSport.Models;
using InnoSport.Data;

namespace InnoSport.Views.Тренер
{
    public partial class TrainerEditAccountInformation : Window
    {
        private User trainer;

        public TrainerEditAccountInformation(User trainer)
        {
            InitializeComponent();
            this.trainer = trainer;

            SurnameTextBox.Text = trainer.Surname;
            NameTextBox.Text = trainer.Name;
            OtchestvoTextBox.Text = trainer.Otchestvo ?? "";
            PhoneNumberTextBox.Text = trainer.PhoneNumber;
            EmailTextBox.Text = trainer.Email ?? "";
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

            using (var db = new AppDBContext())
            {
                var dbTrainer = db.Users.FirstOrDefault(u => u.Id == trainer.Id);
                if (dbTrainer == null)
                {
                    MessageBox.Show("Пользователь не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                dbTrainer.Surname = SurnameTextBox.Text.Trim();
                dbTrainer.Name = NameTextBox.Text.Trim();
                dbTrainer.Otchestvo = OtchestvoTextBox.Text.Trim();
                dbTrainer.PhoneNumber = PhoneNumberTextBox.Text.Trim();
                dbTrainer.Email = EmailTextBox.Text.Trim();

                db.SaveChanges();

                trainer.Surname = dbTrainer.Surname;
                trainer.Name = dbTrainer.Name;
                trainer.Otchestvo = dbTrainer.Otchestvo;
                trainer.PhoneNumber = dbTrainer.PhoneNumber;
                trainer.Email = dbTrainer.Email;
            }

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