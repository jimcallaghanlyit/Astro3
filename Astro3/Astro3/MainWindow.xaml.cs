﻿using AstroLibrary;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
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

        LoginProcess loginProcess = new LoginProcess();

        public MainWindow()
        {
            InitializeComponent();
        }


        /// <summary>
        /// User selects OK at logon screen prompts some back end code to 
        /// run and verify that credentials added are valid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            User validatedUser = new User();
            try
            {
                bool credentialsValidated = false;
                string currentUser = tbxUsername.Text;
                string currentPassword = tbxPassword.Password;
                credentialsValidated = loginProcess.ValidateUserInput(currentUser, currentPassword);
                if (credentialsValidated)
                {
                    validatedUser = GetUserRecord(currentUser, currentPassword);
                    if (validatedUser.User_ID > 0)
                    {
                        CreateLogEntry("Login", "User logged in successfully.", validatedUser.User_ID, validatedUser.Username);
                        Dashboard dashboard = new Dashboard();
                        dashboard.user = validatedUser;
                        this.Close();
                        dashboard.ShowDialog();
                    }
                    else
                    {
                        CreateLogEntry("Login", "The credentials entered do not exist in the database", 0, currentUser);
                        MessageBox.Show("The credentials you entered do not exist in the database.  Please check and try again", "User login", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    CreateLogEntry("Login", "User failed to log in successfully.", 0, currentUser);
                    MessageBox.Show("Error with your username or password.  Please check and try again.", "User login", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
            }
            
        }





        /// <summary>
        /// Validates the user credentials against those in the SQL database.
        /// </summary>
        /// <param name="username">
        /// Username enterued by the User.
        /// </param>
        /// <param name="password">
        /// Password entered by the User.
        /// </param>
        /// <returns>
        /// Validated User.
        /// </returns>
        private User GetUserRecord(string username, string password)
        {
            User validatedUser = new User();
            try
            {
                // Gets the username and password passed to the method
                // from the User table in the SQL database

                foreach (var user in db.Users.Where(t => t.Username == username && t.Password == password))
                {
                    validatedUser = user;
                }
            }
            catch(EntityException ex)
            {
                MessageBox.Show("Problem connecting to the SQL server. Application will not close. See exception " + ex.InnerException, "Connect to Database.", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
                Environment.Exit(0);
            }
            return validatedUser;
        }



        /// <summary>
        /// Creates log entries in the database
        /// </summary>
        /// <param name="category">
        /// Determines the category of the event
        /// </param>
        /// <param name="description">
        /// Description of the event
        /// </param>
        /// <param name="userID">
        /// The userID that generates the event
        /// </param>
        /// <param name="userName">
        /// The Username that generates the event
        /// </param>
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



        /// <summary>
        /// Saves the events to the log file
        /// </summary>
        /// <param name="log">
        /// Generated event is saved to the log file
        /// </param>
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

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterUser register = new RegisterUser();
            register.ShowDialog();
        }

    }
}
