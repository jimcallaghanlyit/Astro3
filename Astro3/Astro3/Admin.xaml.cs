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

        //Added for the Analysis tab
        List<User> userList = new List<User>();
        List<AccessLevel> accesslevelList = new List<AccessLevel>();
        List<Booking> bookingList = new List<Booking>();


        User selectedUser = new User();

        enum DBOperation
        {
            Add,
            Modify,
            Delete
        }

        DBOperation dbOperation = new DBOperation();

        //Define enum type for Summary in Analysis
        enum AnalysisType
        {
            Summary
        }

        private AnalysisType analysisType = new AnalysisType();

        //Define enum type for DB Table selected in Analysis
        enum DBTableSelected
        {
            Users,
            AccessLevel,
            Bookings
        }

        private DBTableSelected tableSelected = new DBTableSelected();


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


            foreach (var userRecord in db.Users)
            {
                userList.Add(userRecord);
            }

            foreach (var bookingRecord in db.Bookings)
            {
                bookingList.Add(bookingRecord);
            }

            foreach (var accesslevelRecord in db.AccessLevels)
            {
                accesslevelList.Add(accesslevelRecord);
            }
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

        private void cboAnalysisType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Check that an option has been selected 
            //The selectedIndex will be 0 if an option is not selected
            if (cboAnalysisType.SelectedIndex > 0)
            {
                if (cboAnalysisType.SelectedIndex == 1)
                {
                    analysisType = AnalysisType.Summary;
                }
            }
        }

        private void cboDatabaseTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Check that an option has been selected 
            //The selectedIndex will be 0 if an option is not selected
            if (cboDatabaseTable.SelectedIndex > 0)
            {
                if (cboDatabaseTable.SelectedIndex == 1)
                {
                    tableSelected = DBTableSelected.Users;
                }

                if (cboDatabaseTable.SelectedIndex == 2)
                {
                    tableSelected = DBTableSelected.AccessLevel;
                }

                if (cboDatabaseTable.SelectedIndex == 3)
                {
                    tableSelected = DBTableSelected.Bookings;
                }
            }
        }

        private void btnAnalyse_Click(object sender, RoutedEventArgs e)
        {
            //Clear variables. Record count is used to display
            //count value beside each record
            int recordCount = 0;
            string output = "";
            tbxAnalysisOutput.Text = "";


            //Display Users list in Analyse table
            if (analysisType == AnalysisType.Summary && tableSelected == DBTableSelected.Users)
            {
                foreach (var item in userList)
                {
                    recordCount++;
                    output = output + Environment.NewLine + $"Record {recordCount} is for user " +
                        $"named {item.Firstname} {item.Surname}" + Environment.NewLine;
                    
                }
                output = output + Environment.NewLine + $"Total User records = {recordCount}" + Environment.NewLine;
                tbxAnalysisOutput.Text = output;                               
            }

            //Display Bookings list in Analyse table
            if (analysisType == AnalysisType.Summary && tableSelected == DBTableSelected.Bookings)
            {
                foreach (var item in bookingList)
                {
                    recordCount++;
                    output = output + Environment.NewLine + $"Record {recordCount} is for booking " +
                        $"ID {item.Booking_ID} created by {item.Created_By} for {item.Slot_Date}" + Environment.NewLine; 
                }
                output = output + Environment.NewLine + $"Total Booking records = {recordCount}" + Environment.NewLine;
                tbxAnalysisOutput.Text = output;
            }


            //Display Access level list in Analyse table
            if (analysisType == AnalysisType.Summary && tableSelected == DBTableSelected.AccessLevel)
            {
                foreach (var item in accesslevelList)
                {
                    recordCount++;
                    output = output + Environment.NewLine + $"Record {recordCount} is for Level ID {item.Level_ID} " +
                        $"which is Job Role: {item.Job_Role}" + Environment.NewLine;
                }
                output = output + Environment.NewLine + $"Total Access level records = {recordCount}" + Environment.NewLine;
                tbxAnalysisOutput.Text = output;
            }

        }
    }
}

