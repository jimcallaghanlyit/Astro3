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

        AstroDBEntities db = new AstroDBEntities();

        List<User> users = new List<User>();
        List<Log> logs = new List<Log>();

        public Admin()
        {
            InitializeComponent();
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lstUserList.ItemsSource = users;
            lstLogList.ItemsSource = logs;
            foreach (var user in db.Users)
            {
                users.Add(user);
            }

            foreach (var log in db.Logs)
            {
                logs.Add(log);
            }
            cboEditUserAccesslevel.SelectedIndex = 0;
        }



        private void submenuAddNewUser_Click(object sender, RoutedEventArgs e)
        {
            stkUserDetails.Visibility = Visibility.Visible;
        }



        private void submenuModUser_Click(object sender, RoutedEventArgs e)
        {
            stkUserDetails.Visibility = Visibility.Visible;
            
        }

        private void submenuDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            
        }



        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            stkUserDetails.Visibility = Visibility.Collapsed;
        }




        private void cboEditUserAccesslevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var theComboBox = (ComboBox)sender;
            ComboBoxItem item = (ComboBoxItem)theComboBox.SelectedItem;
            string value = item.Content.ToString();
            //MessageBox.Show("Content of combobox is " + value);
        }
    }
}
