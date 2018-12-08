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

        User selectedUser = new User();

        enum DBOperation
        {
            Add,
            Modify,
            Delete
        }

        DBOperation dbOperation = new DBOperation();



        public Admin()
        {
            InitializeComponent();
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshUserList();
            lstLogList.ItemsSource = logs;


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
            dbOperation = DBOperation.Modify;   

            
        }





        private void submenuDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            db.Users.RemoveRange(db.Users.Where(t => t.User_ID == selectedUser.User_ID));
            int saveSuccess = db.SaveChanges();
            if(saveSuccess == 1)
            {
                MessageBox.Show("User deleted successfully.", "Save to database", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshUserList();
                ClearUserDetails();
                stkUserDetails.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Problem deleting User record.", "Delete from database", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        private void cboEditUserAccesslevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var theComboBox = (ComboBox)sender;
            ComboBoxItem item = (ComboBoxItem)theComboBox.SelectedItem;
            string value = item.Content.ToString();
            //MessageBox.Show("Content of combobox is " + value);
        }


        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dbOperation == DBOperation.Add)
            {
                User user = new User();
                user.Username = tbxUsername.Text.Trim();
                user.Password = tbxPassword.Text.Trim();
                user.Surname = tbxSurname.Text.Trim();
                user.Firstname = tbxFirstname.Text.Trim();
                user.Club = tbxClub.Text.Trim();
                user.Email = tbxEmail.Text.Trim();
                user.Level_ID = cboEditUserAccesslevel.SelectedIndex;
                int saveSuccess = SaveUser(user);
                if (saveSuccess == 1)
                {
                    MessageBox.Show("User saved successfully.", "Save to Database.", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshUserList();
                    ClearUserDetails();
                    stkUserDetails.Visibility = Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("Problem saving user record.", "Save to Database.", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            if (dbOperation == DBOperation.Modify)
            {
                //For every User in the DB equal to the currently selected User record, then update the record
                foreach (var user in db.Users.Where(t => t.User_ID == selectedUser.User_ID))
                {
                    user.Username = tbxUsername.Text.Trim();
                    user.Password = tbxPassword.Text.Trim();
                    user.Surname = tbxSurname.Text.Trim();
                    user.Firstname = tbxFirstname.Text.Trim();
                    user.Club = tbxClub.Text.Trim();
                    user.Email = tbxEmail.Text.Trim();
                    user.Level_ID = cboEditUserAccesslevel.SelectedIndex;
                }

                int saveSuccess = db.SaveChanges();
                if(saveSuccess == 1)
                {
                    MessageBox.Show("User modified successfully.", "Save to Database.", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshUserList();
                    ClearUserDetails();
                    stkUserDetails.Visibility = Visibility.Collapsed;
                }
            }

        }

        public int SaveUser(User user)
        {
            db.Entry(user).State = System.Data.Entity.EntityState.Added;
            int SaveSuccess = db.SaveChanges();
            return SaveSuccess;
        }


        // Refresh User list in database
        private void RefreshUserList()
        {
            lstUserList.ItemsSource = users;
            users.Clear();
            foreach (var user in db.Users)
            {
                users.Add(user);
            }
            lstUserList.Items.Refresh();
        }


        // Clear Text boxes for adding Users
        private void ClearUserDetails()
        {
            tbxUsername.Text = "";
            tbxPassword.Text = "";
            tbxSurname.Text = "";
            tbxFirstname.Text = "";
            tbxClub.Text = "";
            tbxEmail.Text = "";
            cboEditUserAccesslevel.SelectedIndex = 0;
        }


        //
        private void lstUserList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstUserList.SelectedIndex > 0)
            {
                selectedUser = users.ElementAt(lstUserList.SelectedIndex);
                submenuModUser.IsEnabled = true;
                submenuDeleteUser.IsEnabled = true;
                if (dbOperation == DBOperation.Add)
                {
                    ClearUserDetails();
                }
                if (dbOperation == DBOperation.Modify)
                {

                    tbxUsername.Text = selectedUser.Username;
                    tbxPassword.Text = selectedUser.Password;
                    tbxSurname.Text = selectedUser.Surname;
                    tbxFirstname.Text = selectedUser.Firstname;
                    tbxClub.Text = selectedUser.Club;
                    tbxEmail.Text = selectedUser.Email;
                    cboEditUserAccesslevel.SelectedIndex = selectedUser.Level_ID;
                }

            }
        }
    }
}
