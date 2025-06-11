using System.Windows;
using InnoSport.Models;
using InnoSport.Data;

namespace InnoSport.Views.Администратор
{
    public partial class AdministratorAddSectionWindow : Window
    {
        public AdministratorAddSectionWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                string.IsNullOrWhiteSpace(TypeTextBox.Text) ||
                string.IsNullOrWhiteSpace(DescriptionTextBox.Text))
            {
                MessageBox.Show("Заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using var db = new AppDBContext();
            var section = new Section
            {
                Name = NameTextBox.Text.Trim(),
                Type = TypeTextBox.Text.Trim(),
                Description = DescriptionTextBox.Text.Trim()
            };
            db.Sections.Add(section);
            db.SaveChanges();

            MessageBox.Show("Секция успешно добавлена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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