using IntroSE.Kanban.Frontend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Security;

namespace IntroSE.Kanban.Frontend.ViewModel
{
    class RegisterVM : NotifiableObject
    {
        public UserController Controller { get; private set; }

        private string email;
        public string Email
        {
            get => email;
            set
            {
                this.email = value;
                RaisePropertyChanged("Email");
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

        public RegisterVM(BackendController backendController)
        {
            this.Controller = new UserController(backendController.Service);         
        }

        /// <summary>
        /// try to register by the and password that the user wrote.
        /// </summary>
        public void Register()
        {
            Message = " ";
            try
            {
                Controller.Register(Email, Password);
                Message = "Register completed succesfully. You can Login to your account.";
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
        }
    }
}
