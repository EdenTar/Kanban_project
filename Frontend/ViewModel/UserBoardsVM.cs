using IntroSE.Kanban.Frontend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Frontend.ViewModel
{
    class UserBoardsVM : NotifiableObject
    {
        private BoardController boardController;
        public BoardController BoardController 
        { 
            get=> this.boardController;
            private set
            {
                boardController = value;
            }
        }
        private UserController userController;
        public UserController UserController
        {
            get => this.userController;
            private set
            {
                userController = value;
            }
        }

        private UserM userM;
        public UserM UserM
        {
            get => userM;
            set 
            { userM = value; }
        }

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
        private string boardNameAddBoard;
        public string BoardNameAddBoard
        {
            get => boardNameAddBoard;
            set
            {
                this.boardNameAddBoard = value;
                RaisePropertyChanged("BoardNameAddBoard");
            }
        }
        
        private UserBoardsM userBoardsM;
        public UserBoardsM UserBoardsM { get => userBoardsM; set { userBoardsM = value; } }

        public string Title { get; private set; }
        private bool enableAllBoard = false;
        public bool EnableAllBoard
        {
            get => enableAllBoard;
            set
            {
                enableAllBoard = value;
                RaisePropertyChanged("EnableAllBoard");
            }
        }
        private bool enableMyBoard = false;
        public bool EnableMyBoard
        {
            get => enableMyBoard;
            set
            {                
                enableMyBoard = value;
                RaisePropertyChanged("EnableMyBoard");
            }
        }

        private BoardM selectedMyBoard;
        public BoardM SelectedMyBoard
        {
            get => selectedMyBoard;
            set
            { 
                selectedMyBoard = value;
                selectedAllBoard = null;
                EnableMyBoard = value != null;
                EnableAllBoard = false;
                RaisePropertyChanged("SelectedMyBoard");
                RaisePropertyChanged("SelectedAllBoard");
            }
        }
        private BoardM selectedAllBoard;
        public BoardM SelectedAllBoard
        {
            get => selectedAllBoard;
            set
            {
                selectedAllBoard = value;
                selectedMyBoard = null;
                EnableAllBoard = value != null;
                EnableMyBoard = false;
                RaisePropertyChanged("SelectedAllBoard");
                RaisePropertyChanged("SelectedMyBoard");
            }
        }
        public UserBoardsVM(BackendController backendController, UserM userM)
        {
            this.BoardController = new BoardController(backendController.Service);
            this.UserController = new UserController(backendController.Service);
            this.userM = userM;
            Title = "Hey, " + userM.Email;
            UserBoardsM = userM.getMyBoards();
        }

        /// <summary>
        /// logout the user from his account
        /// </summary>
        public void Logout()
        {
            try
            {
                UserController.Logout(Email);
                Message = "";
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
        }
        /// <summary>
        /// remove the board that the user chose (from MyBoards list)
        /// </summary>
        public void RemoveBoard()
        { 
            try
            {
                UserBoardsM.RemoveBoard(SelectedMyBoard);
                Message = "";
            }
            catch(Exception e)
            {
                Message = (e.Message);
            }
        }

        /// <summary>
        /// enter the board that the user chose (from MyBoards list)
        /// </summary>
        public BoardM EnterBoard()
        {
            try
            {
                Message = "";
                return SelectedMyBoard;
            }
            catch (Exception e)
            {
                Message = (e.Message);
                return null;
            }
        }

        /// <summary>
        /// add board (the name of the board was wrriten in the textbox)
        /// </summary>
        public void AddBoard()
        {
            try
            {
                UserBoardsM.AddBoard(BoardNameAddBoard);
                Message = "";
            }
            catch (Exception e)
            {
                Message = (e.Message);
            }
        }

        /// <summary>
        /// join the board that the user chose (from AllBoards list)
        /// </summary>
        public void JoinBoard()
        {
            try
            {
                UserBoardsM.JoinBoard(SelectedAllBoard);
                Message = "";
            }
            catch (Exception e)
            {
                Message = ("cannot join message. " + e.Message);
            }
        }
    }
}
