using System.Windows;
using InnoSport.Models;
using InnoSport.Data;

namespace InnoSport.Views.Главный_администратор
{
    public partial class ChiefAdministratorEditSectionWindow : Window
    {
        private Section section;

        public ChiefAdministratorEditSectionWindow(Section section)
        {
            InitializeComponent();
            this.section = section;

            NameTextBox.Text = section.Name;
            TypeTextBox.Text = section.Type;
            DescriptionTextBox.Text = section.Description;
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
            var dbSection = db.Sections.FirstOrDefault(s => s.Id == section.Id);
            if (dbSection == null)
            {
                MessageBox.Show("Секция не найдена.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dbSection.Name = NameTextBox.Text.Trim();
            dbSection.Type = TypeTextBox.Text.Trim();
            dbSection.Description = DescriptionTextBox.Text.Trim();

            db.SaveChanges();

            section.Name = dbSection.Name;
            section.Type = dbSection.Type;
            section.Description = dbSection.Description;

            MessageBox.Show("Секция успешно обновлена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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