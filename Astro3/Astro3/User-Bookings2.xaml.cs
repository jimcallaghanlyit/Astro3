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
    /// Interaction logic for User_Bookings2.xaml
    /// </summary>
    public partial class User_Bookings2 : Window
    {
        int frequency=0;// 0 is no reapt 
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


        public User_Bookings2()
        {
            InitializeComponent();
        }

       

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (Weekly.IsChecked == true)
            {
                frequency = 2;
            }
            else
            {
                frequency = 1;
            }

            if (Monday.IsChecked==true)
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

            this.Close();
        }

       

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
