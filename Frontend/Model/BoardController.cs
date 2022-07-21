using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = IntroSE.Kanban.Backend.ServiceLayer.Task;

namespace IntroSE.Kanban.Frontend.Model
{
    class BoardController : BackendController
    {
        public BoardController(Service service) : base(service) { }

        /// <summary>
        /// get the list of boards that have a connection to userM. 
        /// </summary>
        /// <param name="userM">The object of the user that called this function.</param>
        /// <returns>returns a list of BoardM with all the board objects the user created or joined.
        public List<BoardM> GetMyBoardsIdentifier(UserM userM)
        {
            Response<IList<BoardIdentifier>> boards = Service.GetMyBoardsIdentifier(userM.Email);
            if (boards.ErrorOccured)
            {
                return new List<BoardM>();
            }
            return new List<BoardM>(boards.Value.Select((b) => new BoardM(this, b.CreatorEmail, userM, b.Name))).ToList();
        }

        /// <summary>
        /// get the list of boards that don't have a connection to userM. 
        /// </summary>
        /// <param name="userM">The object of the user that called this function.</param>
        /// <returns>returns a list of BoardM with all the board objects the user don't have connecton to.
        public List<BoardM> GetAllBoardsIdentifier(UserM userM)
        {
            Response<IList<BoardIdentifier>> boards = Service.GetAllBoardsIdentifier(userM.Email);
            if (boards.ErrorOccured)
            {
                return new List<BoardM>();
            }
            return new List<BoardM>(boards.Value.Select((b) => new BoardM(this, b.CreatorEmail, userM, b.Name))).ToList();
        }

        /// <summary>
        /// Creates a new board for the recieved user.
        /// </summary>
        /// <param name="userM">The object of the user that try to addBoard</param>
        /// <param name="boardName">The name of the new board</param>
        /// <returns>A BoardM object. if errorOccured, throw exception</returns>
        public BoardM AddBoard(UserM userM, string boardName)
        {
            Response board = Service.AddBoard(userM.Email, boardName);
            if (board.ErrorOccured)
                throw new Exception(board.ErrorMessage);
            return new BoardM(this, userM.Email, userM, boardName);
        }

        /// <summary>
        /// remove board.
        /// </summary>
        /// <param name="userM">The object of the user that try to removeBoard</param>
        /// <param name="creatorEmail">The name of the creator of the board that we will remove</param>
        /// /// <param name="boardName">The name of the board that we will remove</param>
        public void RemoveBoard(UserM userM, string creatorEmail, string boardName)
            {
                Response board = Service.RemoveBoard(userM.Email, creatorEmail, boardName);
                if (board.ErrorOccured)
                    throw new Exception(board.ErrorMessage);
            }

        /// <summary>
        /// join board the belong to other user.
        /// </summary>
        /// <param name="userM">The object of the user that try to join board</param>
        /// <param name="creatorEmail">The name of the creator of the board that we will join to</param>
        /// /// <param name="boardName">The name of the board that we will join to</param>
        public void JoinBoard(UserM userM, string creatorEmail, string boardName)
        {
            Response board = Service.JoinBoard(userM.Email, creatorEmail, boardName);
            if (board.ErrorOccured)
                throw new Exception(board.ErrorMessage);
        }

        public BoardM GetBoard(BoardM boardM)
        {
            Response<Board> board = Service.GetBoard(boardM.UserEmail, boardM.CreatorEmail, boardM.BoardName);
            if (board.ErrorOccured)
                throw new Exception(board.ErrorMessage);
            ObservableCollection<ColumnM> columns = new ObservableCollection<ColumnM>();
            foreach (Column c in board.Value.Columns) 
            {
                ColumnM column = new ColumnM(new ColumnController(this.Service),c.Name, c.Limit, c.OrdinalNumber,boardM,boardM.UserM);
                ObservableCollection<TaskM> tasks = new ObservableCollection<TaskM>();
                foreach (Task t in c.Tasks) 
                {
                    tasks.Add(new TaskM(new TaskController(this.Service), t.Id, t.Title, t.Description, t.emailAssignee, t.DueDate, t.CreationTime, column));
                }
                column.Tasks = tasks;
                columns.Add(column);
            }
            boardM.Columns = columns;
            return boardM;
        }
    }
}

