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
using AstroLibrary;

namespace Astro3
{
    /// <summary>
    /// Interaction logic for Bookings.xaml
    /// </summary>
    public partial class Bookings : Page
    {
        AstroDBEntities db = new AstroDBEntities();
        Booking newbooking = new Booking();
        public User user = new User();



        public Bookings()
        {
            InitializeComponent();
            populateAvilableSlots(calCalendar.DisplayDate);
        }



        /// <summary>
        /// Determine if Frequency is Daily or Weekly
        /// </summary>
        int frequency = 0;
        public int Frequency
        {
            get
            {
                return frequency;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        List<int> days = new List<int>();
        public List<int> Days
        {
            get
            {
                return days;
            }
        }

       

        /// <summary>
        /// Populates slots available for selection based on date selected
        /// </summary>
        /// <param name="selectedDate">
        /// Takes the value from selected date
        /// </param>
        private void populateAvilableSlots(DateTime selectedDate)
        {
            try
            {
                cboAvailableSlots.Items.Clear();
                var query = db.Bookings.Where(row => row.Slot_Date == selectedDate);
                // Query DB for Bookings 
                List<int> slots = new List<int> { 5, 6, 7, 8, 9, 10, 11 };

                //populate a list with all available slot times 
                if (query.Count() > 0)
                {
                    foreach (var line in query)
                    {
                        slots.Remove(line.Slot_time);
                    }
                }

                foreach (var item in slots)
                {
                    cboAvailableSlots.Items.Add("" + item.ToString() + ":00 PM - " + (item + 1).ToString() + ":00 PM");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
            }
        }



        /// <summary>
        /// Mathod which accepts the Calendar selected date value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            populateAvilableSlots(calCalendar.SelectedDate.Value);
        }



        /// <summary>
        /// Method which confirms the booking based on selected date, time & frequency
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            newbooking.User_ID= user.User_ID;
            newbooking.Updated_By = newbooking.Created_By = user.Username;
            newbooking.Slot_Date = calCalendar.SelectedDate.Value;
            newbooking.Creation_Date = newbooking.Last_Update_Date = DateTime.Today;
            
            string digits = new String(cboAvailableSlots.SelectedItem.ToString().TakeWhile(Char.IsDigit).ToArray());
            newbooking.Slot_time = Int32.Parse(digits);

            if (Recurrence.IsChecked == true)
            {
                if (Weekly.IsChecked == true)
                {
                    //Indicates Weekly has been checked
                    newbooking.Frequency = 2;
                }
                else
                {
                    //Indicates that Daily has been checked
                    newbooking.Frequency = 1;
                }

                if (Monday.IsChecked == true)
                {
                    // 1 for Monday
                    days.Add(1);
                }
                if (Tuesday.IsChecked == true)
                {
                    //2 for Tuesday
                    days.Add(2);
                }
                if (Wednesday.IsChecked == true)
                {
                    //3 for Wednesday
                    days.Add(3);
                }
                if (Thursday.IsChecked == true)
                {
                    //4 for Thursday
                    days.Add(4);
                }
                if (Friday.IsChecked == true)
                {
                    //5 for Friday
                    days.Add(5);
                }

                for (int i = 0; i < days.Count(); i++)
                {
                    if (!String.IsNullOrEmpty(newbooking.days) && newbooking.days.Length > 0) 
                        newbooking.days += ",";
                    //Set the newbooking.days value based on days selected
                    newbooking.days += days[i].ToString();
                }
            }
            else
            {
                //A once-off booking
                newbooking.Frequency = 0;
            }

            //Add booking to database
            db.Bookings.Add(newbooking);
            int saveSuccess = db.SaveChanges();
            if (saveSuccess == 1)
            {
                MessageBox.Show("Booking saved successfully.", "Save to Database.", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }



        /// <summary>
        /// Return to previous screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }



        /// <summary>
        /// Displays Frequency panel if checkbox selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Recurrence_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                //add a string to hold the days numbers
                frequencyPanel.Visibility = Visibility.Visible;
                daysPanel.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
            }

        }


        /// <summary>
        /// Removes Frequency panel if checkbox selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Recurrence_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                frequencyPanel.Visibility = Visibility.Hidden;
                daysPanel.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
            }

        }
    }
}
