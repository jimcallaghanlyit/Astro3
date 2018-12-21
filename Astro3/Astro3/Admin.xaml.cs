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
        //DB Connection
        AstroDBEntities db = new AstroDBEntities();

        //Added for the User tab & used to pull in db information on User
        List<User> users = new List<User>();

        //Added for the Log tab & used to pull in db information on Logs
        List<Log> logs = new List<Log>();

        //Added for the Analysis tab & used to pull in db information for User, Access level & Bookings
        List<User> userList = new List<User>();
        List<AccessLevel> accesslevelList = new List<AccessLevel>();
        List<Booking> bookingList = new List<Booking>();


        User selectedUser = new User();



        /// <summary>
        /// Define enumerator vraiable type based on Administrator selection from options below
        /// </summary>
        enum DBOperation
        {
            Add,
            Modify,
            Delete
        }
        //Create the instance
        DBOperation dbOperation = new DBOperation();




        /// <summary>
        /// Define enumerator vraiable type based on Administrator selection from option below
        /// </summary>
        enum AnalysisType
        {
            Summary
        }
        //Create the instance
        private AnalysisType analysisType = new AnalysisType();




        /// <summary>
        /// Defines enumerator vraiable type based on Administrator selection from options below
        /// </summary>
        enum DBTableSelected
        {
            Users,
            AccessLevel,
            Bookings
        }
        //Create the instance
        private DBTableSelected tableSelected = new DBTableSelected();




        public Admin()
        {
            InitializeComponent();
        }



        /// <summary>
        /// On loading page, Refresh the User information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //On loading page, Refresh the User information from DB
            RefreshUserList();
            //Set the itemsourcve for the Logs
            lstLogList.ItemsSource = logs;

            //Display the log information on screen from DB
            foreach (var log in db.Logs)
            {
                logs.Add(log);
            }
            cboEditUserAccesslevel.SelectedIndex = 0;

            //Display the User information on screen from DB
            foreach (var userRecord in db.Users)
            {
                userList.Add(userRecord);
            }

            //Display the booking information on screen from DB
            foreach (var bookingRecord in db.Bookings)
            {
                bookingList.Add(bookingRecord);
            }

            //Display the Access level information on screen from DB
            foreach (var accesslevelRecord in db.AccessLevels)
            {
                accesslevelList.Add(accesslevelRecord);
            }
        }




        /// <summary>
        /// Method for adding a new User
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void submenuAddNewUser_Click(object sender, RoutedEventArgs e)
        {
            stkUserDetails.Visibility = Visibility.Visible;
        }




        /// <summary>
        /// Method for modifying an existing User
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void submenuModUser_Click(object sender, RoutedEventArgs e)
        {
            stkUserDetails.Visibility = Visibility.Visible;
            dbOperation = DBOperation.Modify;   
        }




        /// <summary>
        /// Method for deleting an existing User
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void submenuDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Remove User where User_ID is equal to the User_ID selected by the Administrator
                db.Users.RemoveRange(db.Users.Where(t => t.User_ID == selectedUser.User_ID));
                int saveSuccess = db.SaveChanges();
                if (saveSuccess == 1)
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
            catch (Exception ex)
            {

                MessageBox.Show(ex.InnerException.ToString());
            }
        }



        /// <summary>
        /// Method for editing the Access level for a User
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboEditUserAccesslevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var theComboBox = (ComboBox)sender;
            ComboBoxItem item = (ComboBoxItem)theComboBox.SelectedItem;
            string value = item.Content.ToString();
        }



        /// <summary>
        /// Method for Updating the database based on edits made by Administrator
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dbOperation == DBOperation.Add)
            {
                try
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
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.ToString());
                }


            }
            if (dbOperation == DBOperation.Modify)
            {
                try
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
                    if (saveSuccess == 1)
                    {
                        MessageBox.Show("User modified successfully.", "Save to Database.", MessageBoxButton.OK, MessageBoxImage.Information);
                        RefreshUserList();
                        ClearUserDetails();
                        stkUserDetails.Visibility = Visibility.Collapsed;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.ToString());
                }
                

            }

        }



        /// <summary>
        /// Method for saving a newly added User back to Database
        /// </summary>
        /// <param name="user">
        /// Saves the new User
        /// </param>
        /// <returns></returns>
        public int SaveUser(User user)
        {
            db.Entry(user).State = System.Data.Entity.EntityState.Added;
            int SaveSuccess = db.SaveChanges();
            return SaveSuccess;
        }


        /// <summary>
        /// Method to update User list with information from Database
        /// </summary>
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


        /// <summary>
        ///  Clear Text boxes for adding Users   
        /// </summary>
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




        /// <summary>
        /// Method for displaying information vased on a valid option selected by the Administrator 
        /// Will make the Modify & Delete options available if User selceted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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


        /// <summary>
        /// Method for displaying information vased on a valid option selected by the Administrator 
        /// Will make the 'Summary' options available if selceted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboAnalysisType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //The selectedIndex will be 0 if an option is not selected
            if (cboAnalysisType.SelectedIndex > 0)
            {
                if (cboAnalysisType.SelectedIndex == 1)
                {
                    analysisType = AnalysisType.Summary;
                }
            }
        }



        /// <summary>
        /// Method for displaying information vased on a valid option selected by the Administrator 
        /// Will make the 'Summary' options available if selceted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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


        /// <summary>
        /// Method for displaying information vased on a valid option selected by the Administrator 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                int level1CountSummary = 0;
                int level2CountSummary = 0;

                foreach (var item in userList)
                {
                    recordCount++;
                    output = output + Environment.NewLine + $"Record {recordCount} is for user " +
                        $"named {item.Firstname} {item.Surname}" + Environment.NewLine;

                    if(item.Level_ID == 1)
                    {
                        level1CountSummary++;
                    }

                    if (item.Level_ID == 2)
                    {
                        level2CountSummary++;
                    }

                }
                output = output + Environment.NewLine + $"Total Users with level 1 access is {level1CountSummary}." + Environment.NewLine;
                output = output + Environment.NewLine + $"Total Users with level 2 access is {level2CountSummary}." + Environment.NewLine;
                output = output + Environment.NewLine + $"total number of records = {recordCount}";
                tbxAnalysisOutput.Text = output;                               
            }



            //Display Bookings list in Analyse table
            if (analysisType == AnalysisType.Summary && tableSelected == DBTableSelected.Bookings)
            {
                foreach (var item in bookingList)
                {
                    recordCount++;
                    output = output + Environment.NewLine + $"Record {recordCount} is for booking " +
                        $"ID {item.Booking_ID} created by {item.Created_By} for {item.Slot_Date} {item.Slot_time}" + Environment.NewLine; 
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

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            stkUserDetails.Visibility = Visibility.Collapsed;
        }
    }
}

