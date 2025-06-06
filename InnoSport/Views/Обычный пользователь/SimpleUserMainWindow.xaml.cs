using System.Windows;
using InnoSport.Models;
using InnoSport.Data;

namespace InnoSport.Views.Обычный_пользователь
{
    public partial class SimpleUserMainWindow : Window
    {
        private int userId;

        public SimpleUserMainWindow(User user)
        {
            InitializeComponent();
            userId = user.Id;
            LoadUserData();
        }

        private void LoadUserData()
        {
            using var db = new AppDBContext();
            var user = db.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                UserFullNameText.Text = $"{user.Surname} {user.Name} {user.Otchestvo}".Trim();
                UserPhoneText.Text = user.PhoneNumber;
                UserEmailText.Text = user.Email ?? "";
            }
            else
            {
                UserFullNameText.Text = "";
                UserPhoneText.Text = "";
                UserEmailText.Text = "";
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void EditProfileButton_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new SimpleUserEditAccountInformation(userId);
            if (editWindow.ShowDialog() == true)
            {
                LoadUserData();
            }
        }
    }
}