﻿using AstroLibrary;
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

        /// <summary>
        /// Method to navigate to Admin page when selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Method to navigate to Bookings when selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBookings_Click(object sender, RoutedEventArgs e)
        {
            Bookings bookings = new Bookings();
            bookings.user = user;
            frmMain.Navigate(bookings);
        }


        /// <summary>
        /// Method to check User access level before making Menu options visible
        /// </summary>
        /// <param name="user"></param>
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

        /// <summary>
        /// Method to navigate to My Bookings when selected
        /// </summary>
        private void MyBookingView()
        {
            MyBookings bookingview = new MyBookings();
            bookingview.user = user;
            frmMain.Navigate(bookingview);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CheckUserAccess(user);
            MyBookingView();
        }


        private void btnMyBookings_Click(object sender, RoutedEventArgs e)
        {
            MyBookings bookingview = new MyBookings();
            bookingview.user = user;
            frmMain.Navigate(bookingview);
        }
    }
}
