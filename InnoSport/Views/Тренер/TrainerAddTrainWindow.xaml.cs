using System;
using System.Linq;
using System.Windows;
using InnoSport.Models;
using InnoSport.Data;

namespace InnoSport.Views.Тренер
{
    public partial class TrainerAddTrainWindow : Window
    {
        private User trainer;

        public TrainerAddTrainWindow(User trainer)
        {
            InitializeComponent();
            this.trainer = trainer;
            LoadSections();
        }

        private void LoadSections()
        {
            using var db = new AppDBContext();
            var sections = db.Sections.Where(s => s.TrainerId == trainer.Id).ToList();
            SectionComboBox.ItemsSource = sections;
            SectionComboBox.DisplayMemberPath = "Name";
            SectionComboBox.SelectedValuePath = "Id";
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SectionComboBox.SelectedItem == null ||
                string.IsNullOrWhiteSpace(TimeTextBox.Text) ||
                string.IsNullOrWhiteSpace(DescriptionTextBox.Text))
            {
                MessageBox.Show("Заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using var db = new AppDBContext();
            var section = (Section)SectionComboBox.SelectedItem;

            var training = new Training
            {
                SectionId = section.Id,
                TrainerId = trainer.Id,
                CreatedAt = DateTime.Now,
                Time = TimeTextBox.Text.Trim(),
                Description = DescriptionTextBox.Text.Trim()
            };

            db.Trainings.Add(training);
            db.SaveChanges();

            MessageBox.Show("Тренировка успешно добавлена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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