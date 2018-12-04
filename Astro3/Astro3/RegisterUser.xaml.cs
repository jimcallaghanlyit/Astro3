using AstroLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core;
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
    /// Interaction logic for RegisterUser.xaml
    /// </summary>
    public partial class RegisterUser : Window
    {

        AstroDBEntities db = new AstroDBEntities();

        public RegisterUser()
        {
            InitializeComponent();
        }


        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            User newuser = new User();
            newuser.Level_ID = 1;

            if (tbxUsername.Text.Length > 0)
            {
                newuser.Username = tbxUsername.Text;
            }
            if (tbxPassword.Text.Length > 0)
            {
                newuser.Password = tbxPassword.Text;
            }
            if (tbxSurname.Text.Length > 0)
            {
                newuser.Surname = tbxSurname.Text;
            }
            if (tbxFirstname.Text.Length > 0)
            {
                newuser.Firstname = tbxFirstname.Text;
            }
            if (tbxClub.Text.Length > 0)
            {
                newuser.Club = tbxClub.Text;
            }
            if (tbxEmail.Text.Length > 0)
            {
                newuser.Email = tbxEmail.Text;
            }



          

            try
            {
                db.Users.Add(newuser);
                db.SaveChanges();
                MessageBox.Show("User Added.", "New User", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch
            {
                MessageBox.Show("Try again.", "Error Creating User", MessageBoxButton.OK, MessageBoxImage.Information);
            }  


            
        }

       

       
    }
}
