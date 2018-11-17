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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        AstroDBEntities db = new AstroDBEntities();


        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            int loginCounter = 0;
            string currentUser = tbxUsername.Text;
            string currentPassword = tbxPassword.Password;
            foreach (var userRecord in db.Users)
            {
                if (userRecord.Username == currentUser && userRecord.Password == currentPassword)
                {
                    Dashboard dashboard = new Dashboard();
                    this.Close();
                    dashboard.ShowDialog();
                }
                else
                {
                    lblErrorMessage.Content = "Username or password is incorrect";
                    loginCounter++;
                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
