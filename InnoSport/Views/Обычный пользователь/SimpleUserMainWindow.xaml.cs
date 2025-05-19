using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using InnoSport.Models;

namespace InnoSport.Views.Обычный_пользователь
{
    public partial class SimpleUserMainWindow : Window
    {
        private User user;
        public SimpleUserMainWindow(User user)
        {
            InitializeComponent();
            this.user = user;
            HelloTextBox.Text = $"Привет, {user.Name}";
        }
    }
}
