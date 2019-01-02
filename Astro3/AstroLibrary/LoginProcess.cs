using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroLibrary
{
    public class LoginProcess
    {

        /// <summary>
        /// Validates the user credentials against those in the SQL database.
        /// </summary>
        /// <param name="username">
        /// Username entered by the user.
        /// </param>
        /// <param name="password">
        /// Password entered by the user.
        /// </param>
        /// <returns>
        /// Validated user.
        /// </returns>
        public bool ValidateUserInput(string username, string password)
        {
            bool validated = true;
            try
            {
                // Check username length & ensure not more 
                // or less than specfified in DB table
                if (username.Length == 0 || username.Length > 30)
                {
                    validated = false;
                }
                // Check each character in Username and 
                // make sure no numbers being used
                foreach (char ch in username)
                {
                    if (ch >= '0' && ch <= '9' || ch == '-')
                    {
                        validated = false;
                    }
                }
                // Check password length & ensure not more 
                // or less than specfified in DB table
                if (password.Length == 0 || password.Length > 30)
                {
                    validated = false;
                }
            }
            catch (Exception)
            {

                validated = false;
            }
            //It is easier to set validated to false
            //inside on on of the checks than it is
            //to validate each check
            return validated;
        }
    }
}
