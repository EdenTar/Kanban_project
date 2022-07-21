using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using log4net;
using log4net.Config;
using System.Reflection;
using System.IO;
using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;

namespace IntroSE.Kanban.Backend.BuisnessLayer
{
    class User
    {
        //Every user contains email address (string) and password (Password)
        private string email;
        public string Email { get => email; set { email = value;} }

        private Password password;

        private bool loggedIn;
        public bool LoggedIn { get => loggedIn; set { loggedIn = value; } }
        
        private UserDTO userDTO;
        public UserDTO UserDTO { get => userDTO;set { userDTO = value;} }

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        ///<summary>
        ///This method initialize new user object.
        ///</summary>
        ///<param name="email">the user e-mail address, used as the username for logging the system.</param>
        ///<param name="password">the user password.</param>
        public User(string email, string password)
        {
            //update password
            this.password = new Password(password);

            //verify email and update it
            if(ValidateEmail(email))
            {
                Email = email;
            }
            else
            {
                log.Error("The email structure that has entered is incorrect");
                throw new Exception($"Email structure incorrect");
            }
            //update loggedin
            LoggedIn = false;

            //update userDTO object
            UserDTO = new UserDTO(email, password);
        }

        ///<summary>
        ///This method allows the user to log-in by comparing the password we received, to the user password.
        ///</summary>
        /// <param name="passWord">The password that we need to compare</param>
        /// <returns>true if we succeeded, false if not</returns>
        public bool Login(string password)
        {
            if (password is null)
            {
                log.Error("password is null");
                throw new Exception("password is null");
            }
            //if the password is correct, change LoggedIn to true and return true. if the password is incorrect, return false.
            if (this.password.ValidatePasswordMatch(password))
            {
                LoggedIn = true;
                return true;
            }
            log.Warn("password is incorrect");
            LoggedIn = false;
            return false;
        }


        ///<summary>This method change the LoggedIn to false (makes the user log out of the system).</summary>
        public void Logout()
        {
            LoggedIn = false;
        }


        ///<summary>This method check if the email is valid (according to the regex we entered).</summary>
        /// <param name="email">The email we want to check by the "email rules"</param>
        /// <returns>true if the email is valid.</returns>
        public bool ValidateEmail(string email)
        {
            /*string strRegex = @"^[\w!#$%&'+-/=?^_`{|}~]+(.[\w!#$%&'+-/=?^_`{|}~]+)*"
            + "@"
            + @"((([-\w]+.)+[a-zA-Z]{2,4})|(([0-9]{1,3}.){3}[0-9]{1,3}))$";*/
            
            string strRegex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(email))
            {
                log.Debug("the email is valid");
                return true;
            }
            else
            {
                log.Warn("the email is not valid");
                return false;
            }
        }
       
    }
}
