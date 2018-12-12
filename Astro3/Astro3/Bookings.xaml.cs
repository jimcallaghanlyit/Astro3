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
        int frequency = 0;// 0 is no reapt 
        public int Frequency
        {
            get
            {
                return frequency;
            }
        }


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
        /// <param name="selectedDate"></param>
        private void populateAvilableSlots(DateTime selectedDate)
        {
            cboAvailableSlots.Items.Clear();
            var query = db.Bookings.Where(row => row.Slot_Date == selectedDate);
            // Query DB for Bookings 
            List<int> slots = new List<int> { 5, 6, 7, 8, 9, 10, 11 };

            //populate a list with all available slot times 
            if (query.Count()> 0)
            {
                foreach (var line in query)
                {
                    slots.Remove(line.Slot_time);
                }
            }

            foreach (var item in slots)
            {
                cboAvailableSlots.Items.Add(""+item.ToString()+ ":00 PM - " + (item+1).ToString() + ":00 PM");
            }
            




        }

        private void calCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            populateAvilableSlots(calCalendar.SelectedDate.Value);
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            newbooking.User_ID= user.User_ID;
            newbooking.Updated_By = newbooking.Created_By = user.Username;
            newbooking.Slot_Date = calCalendar.DisplayDate;
            newbooking.Creation_Date = newbooking.Last_Update_Date = DateTime.Today;
            
            string digits = new String(cboAvailableSlots.SelectedItem.ToString().TakeWhile(Char.IsDigit).ToArray());
            newbooking.Slot_time = Int32.Parse(digits);

            if (Recurrence.IsChecked == true)
            {
                if (Weekly.IsChecked == true)
                {
                    newbooking.Frequency = 2;
                }
                else
                {
                    newbooking.Frequency = 1;
                }

                if (Monday.IsChecked == true)
                {
                    days.Add(1);
                }
                if (Tuesday.IsChecked == true)
                {
                    days.Add(2);
                }
                if (Wednesday.IsChecked == true)
                {
                    days.Add(3);
                }
                if (Thursday.IsChecked == true)
                {
                    days.Add(4);
                }
                if (Friday.IsChecked == true)
                {
                    days.Add(5);
                }

                for (int i = 0; i < days.Count(); i++)
                {
                    if (!String.IsNullOrEmpty(newbooking.days) && newbooking.days.Length > 0) //(newbooking.days.Length > 0)
                        newbooking.days += ",";
                    newbooking.days += days[i].ToString();
                    
                }
            }
            else
            {
                newbooking.Frequency = 0;
            }

            db.Bookings.Add(newbooking);
            int saveSuccess = db.SaveChanges();
            if (saveSuccess == 1)
            {
                MessageBox.Show("Booking saved successfully.", "Save to Database.", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Recurrence_Checked(object sender, RoutedEventArgs e)
        {
            //add a string to hold the days numbers
            frequencyPanel.Visibility = Visibility.Visible;
            daysPanel.Visibility = Visibility.Visible;

        }

        private void Recurrence_Unchecked(object sender, RoutedEventArgs e)
        {
            frequencyPanel.Visibility = Visibility.Hidden;
            daysPanel.Visibility = Visibility.Hidden;
        }
    }
}
