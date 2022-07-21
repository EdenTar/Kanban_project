using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Frontend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Frontend.ViewModel
{
    class LoginVM : NotifiableObject
    {
        public UserController Controller { get; private set; }
       
        private string username;
        public string Username
        {
            get => username;
            set
            {
                this.username = value;
                RaisePropertyChanged("UserName");
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                this.password = value;
                RaisePropertyChanged("Password");
            }
        }

        private string message;
        public string Message
        {
            get => message;
            set
            {
                this.message = value;
                RaisePropertyChanged("Message");
            }
        }

        public LoginVM(BackendController backendController)
        {
            this.Controller = new UserController(backendController.Service);
        }

        /// <summary>
        /// try to Login by the username and password that the user wrote
        /// </summary>
        /// <returns>A UserM object of the current user of null if the user does not exist</returns>
        public UserM Login()
        {
            Message = " ";
            try
            {
                return Controller.Login(Username, Password);
            }
            catch (Exception e)
            {
                Message = e.Message;
                return null;
            }
        }
    }
}