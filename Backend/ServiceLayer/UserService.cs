using IntroSE.Kanban.Backend.BuisnessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using System.Reflection;
using System.IO;

namespace IntroSE.Kanban.Backend.ServiceLayer
{  
    class UserService
    {

        private UserController userController;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initialize UserService object.
        /// </summary>
        /// <param name="userController">The system user controller</param>
        public UserService(UserController userController)
        {
            this.userController = userController;
        }


        /// <summary>
        /// Register a new user into the system.
        /// </summary>
        /// <param name="email">The email of the new user</param>
        /// <param name="password">The password of the new user</param>
        /// <returns>Response which contains error massege in case of error</returns>
        public Response Register(string email, string password)
        {

            try
            {
                log.Info($"A new user is trying to register: email({email})");
                userController.Register(email, password);
                log.Debug($"The registretion for {email} is completed succesfully");
                return new Response();
            }
            catch(Exception e)
            {
                log.Debug("registretion failed");
                return new Response(e.Message);
            }
        }


        /// <summary>
        ///  Login a user to the system.
        /// </summary>
        /// <param name="email">The email of the user logging in</param>
        /// <param name="password">The password of the user logging in</param>
        /// <returns>
        ///  Response object, which contains error message in case of error,
        ///  otherwise a User object of the logged in user.
        /// </returns>
        public Response<User> Login(string email, string password)
        {
            try
            {
                log.Info($"A user is trying to log in");
                userController.Login(email, password);
                log.Debug($"The login is completed succesfully");
                return Response<User>.FromValue(new User(email));
            }
            catch(Exception e)
            {
                log.Debug("Login failed");
                return Response<User>.FromError(e.Message);
            }
        }

        
        /// <summary>
        ///  Logout a user from the system.
        /// </summary>
        /// <param name="email">The email of the user wanting to log out</param>
        /// <returns>Response object containing error message in case of error</returns>
        public Response Logout(string email)
        {
            try
            {
                log.Info($"A user is trying to log out");
                userController.GetUser(email).Logout();
                log.Debug($"The logout is completed succesfully");
                return new Response();
            }
            catch (Exception e)
            {
                log.Debug("Logout failed");
                return new Response(e.Message);
            }
        }
    }
}
