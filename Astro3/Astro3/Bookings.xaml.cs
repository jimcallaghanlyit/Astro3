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
        public Bookings()
        {
            
            InitializeComponent();
            populateAvilableSlots(calCalendar.DisplayDate);
        }

        private void btnBook_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRecurrence_Click(object sender, RoutedEventArgs e)
        {
            User_Bookings2 bookings2 = new User_Bookings2();
            frmBooking.Navigate(bookings2);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void populateAvilableSlots(DateTime selectedDate)
        {
            cboAvailableSlots.Items.Clear();
            var query = db.Bookings.Where(row => row.Slot_Date == selectedDate);
            //todo
            List<int> slots = new List<int> { 5, 6, 7, 8, 9, 10, 11 };

            //populate a list with all slot times 
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
    }
}
