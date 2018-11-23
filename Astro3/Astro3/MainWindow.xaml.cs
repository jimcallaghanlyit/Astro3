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
            User validatedUser = new User();
            bool login = false;
            string currentUser = tbxUsername.Text;
            string currentPassword = tbxPassword.Password;
            foreach (var userRecord in db.Users)
            {
                if (userRecord.Username == currentUser && userRecord.Password == currentPassword)
                {

                    login = true;
                    validatedUser = userRecord;

                }
                else
                {
                    lblErrorMessage.Content = "Username or password is incorrect";
                    loginCounter++;
                }
            }
            
            if (login)
            {
                CreateLogEntry("Login", "User logged in successfully.", validatedUser.User_ID, validatedUser.Username);
                Dashboard dashboard = new Dashboard();
                dashboard.user = validatedUser;
                //dashboard.Owner = this;
                this.Close();
                dashboard.ShowDialog();
                //this.Hide();
            }
            else
            {
                CreateLogEntry("Login", "User did not login.", 0, currentUser);
            }
        }


        private void CreateLogEntry(string category, string description, int userID, string userName)
        {
            string comment = $"{description} user credentials = {userName}";
            Log log = new Log();
            log.User_ID = userID;
            log.Category = category;
            log.Description = comment;
            log.Date = DateTime.Now;
            SaveLog(log);
        }

        private void SaveLog(Log log)
        {
            db.Entry(log).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
        }



        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }
    }
}
