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
    /// Interaction logic for Page1.xaml
    /// </summary>
    /// 
    
   
    public partial class MyBookings : Page
    {
        AstroDBEntities db = new AstroDBEntities();
        List<Booking> bookingList = new List<Booking>();
        //List<Booking> bookingSlot = new List<Booking>();
        Booking selectedSlot = new Booking();
        public User user = new User();
        public Booking booking = new Booking();

        

        public MyBookings()
        {
            InitializeComponent();
        }




        private void populateList()
        {
            foreach (var bookingRecord in db.Bookings.Where(u => u.User_ID == user.User_ID))
            {
                bookingList.Add(bookingRecord);       
            }
            lstBookingList.ItemsSource = bookingList;
            lstBookingList.Items.Refresh();
        }



        // Refresh User list in database
        private void RefreshBookingList()
        {
            lstBookingList.ItemsSource = bookingList;
            lstBookingList.Items.Refresh();
        }



        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            populateList();
        }



        private void submenuDeleteBooking_Click(object sender, RoutedEventArgs e)
        {
            
            db.Bookings.RemoveRange(db.Bookings.Where(u => u.User_ID == user.User_ID));
            int saveSuccess = db.SaveChanges();
            if (saveSuccess == 1)
            {
                MessageBox.Show("Booking slot deleted successfully.", "Save to database", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshBookingList();
            }
            else
            {
                MessageBox.Show("Problem deleting booking slot.", "Delete from database", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        private void lstBookingList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstBookingList.SelectedIndex > 0)
            {
                submenuDeleteBooking.IsEnabled = true;             
            }
        }





    }
}
