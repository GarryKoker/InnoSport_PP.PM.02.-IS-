using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using InnoSport.Models;
using InnoSport.Data;

namespace InnoSport.Views.Тренер
{
    public partial class TrainerMainWindow : Window
    {
        private User trainer;

        public TrainerMainWindow(User trainer)
        {
            InitializeComponent();
            this.trainer = trainer;
            LoadTrainerInfo();
            LoadTrainings();
            LoadSportsmen();
        }

        private void LoadTrainerInfo()
        {
            TrainerNameText.Text = $"{trainer.Surname} {trainer.Name} {trainer.Otchestvo}".Trim();
        }

        private void LoadTrainings()
        {
            using var db = new AppDBContext();
            var trainings = db.Trainings
                .Where(t => t.TrainerId == trainer.Id)
                .OrderBy(t => t.CreatedAt)
                .Select(t => new
                {
                    t.Id,
                    SectionName = db.Sections.FirstOrDefault(s => s.Id == t.SectionId).Name,
                    t.Time,
                    t.Description,
                    Date = t.CreatedAt.ToShortDateString()
                })
                .ToList();

            ScheduleDataGrid.ItemsSource = trainings;
        }

        private void LoadSportsmen()
        {
            using var db = new AppDBContext();
            var sectionIds = db.Sections.Where(s => s.TrainerId == trainer.Id).Select(s => s.Id).ToList();
            var sportsmen = db.UserSections
                .Where(us => sectionIds.Contains(us.SectionId))
                .Select(us => us.UserId)
                .Distinct()
                .Join(db.Users, id => id, u => u.Id, (id, u) => u)
                .Where(u => u.Role == (int)Roles.Sportsman)
                .Select(u => new
                {
                    u.Id,
                    FullName = $"{u.Surname} {u.Name} {u.Otchestvo}".Trim(),
                    u.PhoneNumber,
                    u.Email
                })
                .ToList();

            StudentsDataGrid.ItemsSource = sportsmen;
        }

        private void AddScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            var addTrainWindow = new TrainerAddTrainWindow(trainer);
            if (addTrainWindow.ShowDialog() == true)
            {
                LoadTrainings();
            }
        }

        private void DeleteScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            if (ScheduleDataGrid.SelectedItem is not null)
            {
                dynamic selected = ScheduleDataGrid.SelectedItem;
                int trainingId = selected.Id;

                using var db = new AppDBContext();
                var training = db.Trainings.FirstOrDefault(t => t.Id == trainingId);
                if (training != null)
                {
                    db.Trainings.Remove(training);
                    db.SaveChanges();
                    LoadTrainings();
                    MessageBox.Show("Тренировка удалена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void StudentsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Здесь можно реализовать отображение подробной информации о спортсмене
            // или открытие окна прогресса
        }

        // Новый обработчик: редактирование профиля тренера
        private void EditProfileButton_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new TrainerEditAccountInformation(trainer);
            if (editWindow.ShowDialog() == true)
            {
                LoadTrainerInfo();
            }
        }
    }
}