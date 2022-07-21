using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Frontend.Model
{
    public class UserBoardsM :NotifiableModelObject
    {
        private readonly UserM userM;
        private ObservableCollection<BoardM> myBoards;
        public ObservableCollection<BoardM> MyBoards {
            get => myBoards;
            set
            { myBoards = value; }
        }
        private ObservableCollection<BoardM> allBoards;
        public ObservableCollection<BoardM> AllBoards
        {
            get => allBoards;
            set
            { allBoards = value; }
        }
        public UserBoardsM(BackendController controller, UserM userM) :base(controller)
        {
            this.userM = userM;

            MyBoards = new ObservableCollection<BoardM>((new BoardController(controller.Service)).GetMyBoardsIdentifier(userM));
            MyBoards.CollectionChanged += HandleMyChange;

            AllBoards = new ObservableCollection<BoardM>((new BoardController(controller.Service)).GetAllBoardsIdentifier(userM));
            AllBoards.CollectionChanged += HandleAllChange;
        }

        /// <summary>
        /// Remove board that the user creted and remove it from MyBoards list.
        /// </summary>
        /// <param name="boardM">The BoardM object that we will delete</param>
        public void RemoveBoard(BoardM boardM)
        {
            if (userM.Email.Equals(boardM.CreatorEmail))
            {
                MyBoards.Remove(boardM);
            }
            else
                throw new Exception("A board can only be deleted if you have created it");
        }
        /// <summary>
        /// Creates a new board and add it to MyBoards list.
        /// </summary>
        /// <param name="boardName">The name of the new board</param>
        public void AddBoard(string boardName)
        {
            if (boardName != null && boardName != "")
            {
                foreach (BoardM b in MyBoards)
                {
                    if (b.BoardName == boardName && userM.Email == b.CreatorEmail)
                        throw new Exception("This board already exist"); ;
                }
                BoardM boardM = new BoardM(Controller, userM.Email, userM, boardName);
                MyBoards.Add(boardM);
            }
            else
                throw new Exception("board name can't be empty");
        }

        /// <summary>
        /// join the user to the recieved board(and add it to MyBoards list).
        /// </summary>
        /// <param name="boardM">The BoardM object that we will delete</param>
        public void JoinBoard(BoardM boardM)
        {
            if (!userM.Email.Equals(boardM.CreatorEmail))
            {
                MyBoards.Add(boardM);
                AllBoards.Remove(boardM);
            }
        }

        /// <summary>
        /// handle changes that were made in MyBoards list such as removeBoard and addBoard
        /// </summary>
        private void HandleMyChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (BoardM b in e.OldItems)
                {
                        (new BoardController(Controller.Service)).RemoveBoard(userM, b.CreatorEmail, b.BoardName);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (BoardM b in e.NewItems)
                {
                    if (b.CreatorEmail.Equals(userM.Email))
                    {
                        (new BoardController(Controller.Service)).AddBoard(userM, b.BoardName);
                    }                   
                }
            }
        }
        /// <summary>
        /// handle changes that were made in AllBoards list - joinBoard
        /// </summary>
        private void HandleAllChange(object sender, NotifyCollectionChangedEventArgs e)
        {
           if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (BoardM b in e.OldItems)
                {
                   (new BoardController(Controller.Service)).JoinBoard(userM, b.CreatorEmail, b.BoardName);                    
                }
            }
        }

    }
}
