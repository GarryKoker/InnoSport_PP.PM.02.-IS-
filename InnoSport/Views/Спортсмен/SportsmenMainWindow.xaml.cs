using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using InnoSport.Models;
using InnoSport.Data;

namespace InnoSport.Views.Спортсмен
{
    public partial class SportsmenMainWindow : Window
    {
        private User user;

        public SportsmenMainWindow(User user)
        {
            InitializeComponent();
            this.user = user;
            LoadUserInfo();
            LoadSections();
            LoadSchedule();
            LoadProgressSections();
        }

        private void LoadUserInfo()
        {
            SportsmenNameText.Text = $"{user.Name}";
            SportsmenFioText.Text = $"ФИО: {user.Surname} {user.Name} {user.Otchestvo}";
            SportsmenPhoneText.Text = $"Телефон: {user.PhoneNumber}";
            SportsmenEmailText.Text = $"Email: {user.Email}";
        }

        private void LoadSections()
        {
            using var db = new AppDBContext();
            var sectionIds = db.UserSections
                .Where(us => us.UserId == user.Id)
                .Select(us => us.SectionId)
                .ToList();

            var sections = db.Sections
                .Where(s => sectionIds.Contains(s.Id))
                .Select(s => new
                {
                    s.Id,
                    Name = s.Name,
                    Coach = db.Users.FirstOrDefault(u => u.Id == s.TrainerId) != null
                        ? db.Users.FirstOrDefault(u => u.Id == s.TrainerId).Surname + " " + db.Users.FirstOrDefault(u => u.Id == s.TrainerId).Name
                        : "—",
                    Description = s.Description
                })
                .ToList();

            SectionsDataGrid.ItemsSource = sections;
        }

        private void LoadSchedule()
        {
            using var db = new AppDBContext();
            var sectionIds = db.UserSections
                .Where(us => us.UserId == user.Id)
                .Select(us => us.SectionId)
                .ToList();

            var schedule = db.Trainings
                .Where(t => sectionIds.Contains(t.SectionId))
                .Select(t => new
                {
                    Date = t.CreatedAt.ToShortDateString(),
                    Time = t.Time,
                    SectionName = db.Sections.FirstOrDefault(s => s.Id == t.SectionId).Name,
                    Description = t.Description
                })
                .OrderBy(t => t.Date)
                .ToList();

            ScheduleDataGrid.ItemsSource = schedule;
        }

        private void LoadProgressSections()
        {
            using var db = new AppDBContext();
            var sectionIds = db.UserSections
                .Where(us => us.UserId == user.Id)
                .Select(us => us.SectionId)
                .ToList();

            var sections = db.Sections
                .Where(s => sectionIds.Contains(s.Id))
                .ToList();

            SectionProgressComboBox.ItemsSource = sections;
            SectionProgressComboBox.DisplayMemberPath = "Name";
            SectionProgressComboBox.SelectedValuePath = "Id";
        }

        private void EditProfileButton_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new SportsmenEditInformationWindow(user);
            if (editWindow.ShowDialog() == true)
            {
                LoadUserInfo();
            }
        }

        private void LeaveSectionButton_Click(object sender, RoutedEventArgs e)
        {
            if (SectionsDataGrid.SelectedItem is not null)
            {
                dynamic selectedSection = SectionsDataGrid.SelectedItem;
                int sectionId = selectedSection.Id;

                using var db = new AppDBContext();
                var userSection = db.UserSections.FirstOrDefault(us => us.UserId == user.Id && us.SectionId == sectionId);
                if (userSection != null)
                {
                    // Можно реализовать создание LeaveRequest или сразу удалять связь
                    db.UserSections.Remove(userSection);
                    db.SaveChanges();
                    LoadSections();
                    LoadSchedule();
                    LoadProgressSections();
                    MessageBox.Show("Вы успешно вышли из секции.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void SectionProgressComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Здесь можно реализовать загрузку и отображение графика прогресса по выбранной секции
            // Например, обновить данные для LiveCharts
        }

        private void MarkProgressButton_Click(object sender, RoutedEventArgs e)
        {
            // Открыть окно для отметки прогресса или реализовать логику добавления новой записи в Progress
            MessageBox.Show("Функционал отметки прогресса не реализован.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SectionsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Можно реализовать отображение подробной информации о секции
        }
    }
}