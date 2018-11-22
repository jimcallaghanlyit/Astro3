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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Astro3
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {

        List<User> users = new List<User>();
        public Admin()
        {
            InitializeComponent();
        }

        private void submenuAddNewUser_Click(object sender, RoutedEventArgs e)
        {
            stkUserDetails.Visibility = Visibility.Visible;
        }

        private void submenuModUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void submenuDeleteUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            stkUserDetails.Visibility = Visibility.Collapsed;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lstUserList.ItemsSource = users;
           
        }
    }
}
