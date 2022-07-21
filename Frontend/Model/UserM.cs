using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Frontend.Model
{
    public class UserM : NotifiableModelObject
    {
        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                RaisePropertyChanged("Email");
            }
        }

        public UserM(UserController controller, string email) : base(controller)
        {
            this.Email = email;
        }

        public UserBoardsM getMyBoards()
        {
            return new UserBoardsM(Controller, this);
        }
    }
}