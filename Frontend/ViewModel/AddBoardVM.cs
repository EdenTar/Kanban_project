using IntroSE.Kanban.Frontend.Model;
using IntroSE.Kanban.Frontend.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Frontend.ViewModel
{
    class AddBoardVM : NotifiableObject
    {
        private BoardController boardController;
        public BoardController BoardController
        {
            get => boardController;
            private set
            {
                boardController = value;
                RaisePropertyChanged("id");
            }
        }

        private UserM userM;
        public UserM UserM
        {
            get => userM;
            private set
            {
                userM = value;

            }
        }


        private string creatorEmail;
        public string CreatorEmail { get => creatorEmail; set { creatorEmail = value; } }

        private string boardName;
        public string BoardName { get => boardName; set { boardName = value; } }
        public AddBoardVM(BackendController backendController, UserM userM)
        {
            UserM = userM;
            boardController = new BoardController(backendController.Service);
        }

        /// <summary>
        /// Adds a new board 
        /// </summary>
        public void AddBoard()
        {
            try
            {
                boardController.AddBoard(UserM,BoardName);
            }
            catch(Exception e)
            {
                new PopupErrorV(e.Message).Show();
            }
        }
    }
}
