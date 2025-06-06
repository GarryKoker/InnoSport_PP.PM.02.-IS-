using System.Linq;
using System.Windows;
using InnoSport.Models;
using InnoSport.Data;

namespace InnoSport.Views.Администратор
{
    public partial class AdministratorMainWindow : Window
    {
        private User adminUser;

        public AdministratorMainWindow(User admin = null)
        {
            InitializeComponent();
            adminUser = admin;
            LoadUsers();
            LoadSections();
        }

        private void LoadUsers()
        {
            using var db = new AppDBContext();
            var users = db.Users.ToList();
            UsersDataGrid.ItemsSource = users;
        }

        private void LoadSections()
        {
            using var db = new AppDBContext();
            var sections = db.Sections.ToList();
            SectionsDataGrid.ItemsSource = sections;
        }

        private void EditUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsersDataGrid.SelectedItem is User user)
            {
                var editUserWindow = new AdministratorEditUsersWindow(user);
                if (editUserWindow.ShowDialog() == true)
                    LoadUsers();
            }
        }

        private void AddSectionButton_Click(object sender, RoutedEventArgs e)
        {
            var addSectionWindow = new AdministratorAddSectionWindow();
            if (addSectionWindow.ShowDialog() == true)
                LoadSections();
        }

        private void EditSectionButton_Click(object sender, RoutedEventArgs e)
        {
            if (SectionsDataGrid.SelectedItem is Section section)
            {
                var editSectionWindow = new AdministratorEditSectionWindow(section);
                if (editSectionWindow.ShowDialog() == true)
                    LoadSections();
            }
        }

        private void DeleteSectionButton_Click(object sender, RoutedEventArgs e)
        {
            if (SectionsDataGrid.SelectedItem is Section section)
            {
                var result = MessageBox.Show("Удалить секцию?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    using var db = new AppDBContext();
                    var dbSection = db.Sections.FirstOrDefault(s => s.Id == section.Id);
                    if (dbSection != null)
                    {
                        db.Sections.Remove(dbSection);
                        db.SaveChanges();
                        LoadSections();
                    }
                }
            }
        }

        private void EditProfileButton_Click(object sender, RoutedEventArgs e)
        {
            if (adminUser == null)
            {
                MessageBox.Show("Пользователь не определён.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var editWindow = new AdministratorEditInformationWindow(adminUser);
            if (editWindow.ShowDialog() == true)
            {

            }
        }

        private void ShowInfoButton_Click(object sender, RoutedEventArgs e)
        {
            int usersCount, sectionsCount;
            using (var db = new AppDBContext())
            {
                usersCount = db.Users.Count();
                sectionsCount = db.Sections.Count();
            }
            MessageBox.Show($"Всего пользователей: {usersCount}\nВсего секций: {sectionsCount}", "Основная информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}