using AstroLibrary;
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

namespace Astro3
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {

        public User user = new User();
        public Dashboard()
        {
            InitializeComponent();
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            Admin admin = new Admin();
            frmMain.Navigate(admin);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        private void btnBookings_Click(object sender, RoutedEventArgs e)
        {
            Bookings bookings = new Bookings();
            frmMain.Navigate(bookings);
        }

        private void CheckUserAccess(User user)
        {
            if (user.Level_ID == 1)
            {
                btnBookings.Visibility = Visibility.Visible;
            }

            if (user.Level_ID == 2)
            {
                btnAdmin.Visibility = Visibility.Visible;
                btnBookings.Visibility = Visibility.Visible;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CheckUserAccess(user);
        }
    }
}
