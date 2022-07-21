using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using System.Reflection;
using System.IO;
using IntroSE.Kanban.Backend.DataAccessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;

namespace IntroSE.Kanban.Backend.BuisnessLayer
{
    class UserController
    {
        /// <summary>
        /// Dictionary<email,User>
        /// </summary>
        private Dictionary<string, User> Users;
        private UserDalController userDalController;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        /// <summary>
        /// initialize Users;
        /// </summary>
        internal UserController()
        {
            userDalController = new UserDalController();
            Users = new Dictionary<string, User>();
        }

        /// <summary>
        /// check if the user exists  and if he is logged in.
        /// </summary>
        /// <param name="email">the email of the user</param>
        public void CheckUserLogged(string email)
        {
            if (email is null)
            {
                log.Error("email is null");
                throw new ArgumentNullException(nameof(email));
            }
            if (!GetUser(email).LoggedIn)
            {
                log.Error($"user with email {email} is not logged in the system...");
                throw new Exception($"user with email {email} is not logged in the system...");
            }
        }

        ///<summary>load all the users in the db and insert them to the dictionary</summary>      
        public void LoadUsers()
        {
            List<UserDTO> users = userDalController.LoadAllUsers();
            foreach (UserDTO user in users)
            {
                User u = new User(user.Email, user.Password);
                Users[u.Email] = u;
            }
        }

        ///<summary>delete all the users</summary>      
        public void DeleteUsers()
        {
            userDalController.DeleteAllData();
        }

        ///<summary>This method Register User using email and password.</summary>
        ///<param name="email">the user e-mail address, used as the username for logging the system.</param>
        ///<param name="password">the user password.</param>
        internal void Register(string email, string password)
        {
            if (email == null)
            {
                log.Error("A user with null email attempted to register");
                throw new Exception("Email is null");
            }
            if (password == null)
            {
                log.Error("A user with null Password attempted to register");
                throw new Exception("Password is null");
            }

            //verify that email does not exist
            if (Users.ContainsKey(email))
            {
                log.Error("A user with an email that is already Exists attempted to register");
                throw new Exception($"Email {email} already exists");
            }

            //create new user            
            User newUser = new User(email, password);
            //adding to dictionary of existing users
            Users.Add(email, newUser);

            //adding the new user to db
            newUser.UserDTO.Insert();
        }


        ///<summary>This method return User according to his email.</summary>
        /// <param name="email">The email of the wanted user</param>
        /// <returns>User object according to his email</returns>
        public User GetUser(string email)
        {
            if (email is null)
            {
                log.Error("The email of the user is null");
                throw new Exception("Email is null");
            }

            //verify that email exists
            if (!(Users.ContainsKey(email)))
            {
                log.Error($"Email {email} does not exist");
                throw new Exception($"Email {email} does not exist");
            }

            return Users[email];
        }


        ///<summary>
        ///This method allows the user to log-in by comparing the information we received, 
        ///with the user information in the dictionary.
        ///</summary>
        ///<param name="email">the user e-mail address, used as the username for logging the system.</param>
        ///<param name="password">the user password.</param>
        /// <returns>User object according to his email and password</returns>
        public User Login(string email, string password)
        {
            IsThereUserLoggedIn();
            //get user object (by email)
            User user = this.GetUser(email);

            //verify the password and update loggedIn by calling Login method in User class. 
            if (user.Login(password))
            {
                return user;
            }
            else
            {
                log.Error("The password that the system recieved is incorrect");
                throw new Exception("The password is incorrect");
            }
        }

        /// <summary>
        /// Checks if there is user logged in.
        /// </summary>
        public void IsThereUserLoggedIn()
        {
            foreach (User user in this.Users.Values.ToList()) 
            {
                if (user.LoggedIn) 
                {
                    log.Error($"There is other user({user.Email}) logged in already...");
                    throw new Exception($"There is other user({user.Email}) logged in already...");
                }
            }            
        }
    }
}
