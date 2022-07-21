using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Frontend.Model
{
    public class UserController : BackendController
    {

        public UserController(Service service) : base(service)
        {

        }

        /// <summary>
        /// Turns to the service in order to login.
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="password">The password that the user enered</param>
        /// <returns>Returns a new Model object of User</returns>
        public UserM Login(string username, string password)
        {
            Response<User> user = Service.Login(username, password);
            if (user.ErrorOccured)
                throw new Exception(user.ErrorMessage);
            return new UserM(this, username);
        }
        
        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="userEmail">The email address of the user to register</param>
        /// <param name="password">The password of the user to register</param>
        public void Register(string userEmail, string password)
        {
            Response user = Service.Register(userEmail, password);
            if (user.ErrorOccured)
                throw new Exception(user.ErrorMessage);
        }

        /// <summary>        
        /// Log out the user from the system. 
        /// </summary>
        /// <param name="userEmail">The email of the user to log out</param>
        public void Logout(string userEmail)
        {
            Response user = Service.Logout(userEmail);
            if (user.ErrorOccured)
                throw new Exception(user.ErrorMessage);
        }

    }
}
