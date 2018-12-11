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
        public User user = new User();
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

            lstUserList.ItemsSource = bookingList;
            lstUserList.Items.Refresh();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            populateList();
        }
    }
}
