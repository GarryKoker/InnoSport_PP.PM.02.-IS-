using System.Linq;
using System.Windows;
using InnoSport.Models;
using InnoSport.Data;

namespace InnoSport.Views.Главный_администратор
{
    public partial class ChiefAdministratorMainWindow : Window
    {
        public ChiefAdministratorMainWindow()
        {
            InitializeComponent();
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

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            var addUserWindow = new ChiefAdministratorAddUserWindow();
            if (addUserWindow.ShowDialog() == true)
                LoadUsers();
        }

        private void EditUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsersDataGrid.SelectedItem is User user)
            {
                var editUserWindow = new ChiefAdministratorEditUserWindow(user);
                if (editUserWindow.ShowDialog() == true)
                    LoadUsers();
            }
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsersDataGrid.SelectedItem is User user)
            {
                var result = MessageBox.Show("Удалить пользователя?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    using var db = new AppDBContext();
                    var dbUser = db.Users.FirstOrDefault(u => u.Id == user.Id);
                    if (dbUser != null)
                    {
                        db.Users.Remove(dbUser);
                        db.SaveChanges();
                        LoadUsers();
                    }
                }
            }
        }

        private void AddSectionButton_Click(object sender, RoutedEventArgs e)
        {
            var addSectionWindow = new ChiefAdministratorAddSectionWindow();
            if (addSectionWindow.ShowDialog() == true)
                LoadSections();
        }

        private void EditSectionButton_Click(object sender, RoutedEventArgs e)
        {
            if (SectionsDataGrid.SelectedItem is Section section)
            {
                var editSectionWindow = new ChiefAdministratorEditSectionWindow(section);
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
            // Открыть окно редактирования профиля главного администратора (реализовать передачу текущего пользователя)
        }

        private void ShowReportsButton_Click(object sender, RoutedEventArgs e)
        {
            var reportsWindow = new ChiefAdministratorSeeReportsWindow();
            reportsWindow.ShowDialog();
        }
    }
}