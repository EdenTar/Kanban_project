using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using System.Reflection;
using System.IO;
using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using IntroSE.Kanban.Backend.DataAccessLayer;
using IntroSE.Kanban.DataAccessLayer;
namespace IntroSE.Kanban.Backend.BuisnessLayer
{
    class BoardController
    {
        // Dictionary<email,Dictionary<board name, Board object>
        private Dictionary<string, Dictionary<string, Board>> UsersBoards;

        // Dictionary<email,List<Board object>>
        private Dictionary<string, List<Board>> membership;
        public Dictionary<string, List<Board>> Membership { get => membership; set { membership = value; } }

        private UserController userController;
        public UserController UserController { get => userController; set { userController = value; } }
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private long idGenerator;
        public long IdGenerator { get => idGenerator; set { idGenerator = value; } }

        /// <summary>
        /// initialize BoardController object.
        /// </summary>
        internal BoardController(UserController userController)
        {
            IdGenerator = 0;
            Membership = new Dictionary<string, List<Board>>();
            UsersBoards = new Dictionary<string, Dictionary<string, Board>>();
            UserController = userController;
            Membership = new Dictionary<string, List<Board>>();
        }

        /// <summary>
        /// Add new membership between userEmail and board
        /// </summary>
        /// <param name="userEmail">the email of the user have new membership</param>
        /// <param name="board">the board to membership to.</param>
        public void AddMembership(string userEmail, Board board)
        {
            if (!Membership.ContainsKey(userEmail))
            {
                Membership.Add(userEmail, new List<Board>());
            }
            else if (Membership[userEmail].Contains(board))
            {
                throw new Exception("trying to add the same membership more then once");
            }
            Membership[userEmail].Add(board);
        }

        /// <summary>
        /// Remove membership between userEmail and board
        /// </summary>
        /// <param name="userEmail">the email to remove membership from</param>
        /// <param name="board">the board to remove membership to</param>
        public void RemoveMembership(string userEmail, Board board)
        {
            if ((!Membership.ContainsKey(userEmail)) || (!Membership[userEmail].Contains(board)))
            {
                throw new Exception("can't delete membership that doesn't exist");
            }
            Membership[userEmail].Remove(board);
        }

        /// <summary>
        /// returns the names of all the boards the user has created or joined.
        /// </summary>
        /// <param name="userEmail">the email whom board names are returned</param>
        /// <returns>list containing the names of all the boards</returns>
        public List<string> GetBoardNames(string userEmail)
        {
            UserController.CheckUserLogged(userEmail);
            List<string> boardNames = new List<string>();
            foreach (Board board in Membership[userEmail])
            {
                boardNames.Add(board.Name);
            }
            return boardNames;
        }

        ///<summary>
        ///load all the Boards from the db
        ///</summary>      
        public void LoadBoards()
        {
            int max = 0;
            //get a list of all the boards in db
            List<BoardDTO> boards = (new BoardDalController()).LoadAllBoards();
            foreach (BoardDTO boardDTO in boards)
            {
                Board board = new Board(boardDTO);

                if (Convert.ToInt32(board.BoardId.Substring(1)) > max)
                {
                    max = Convert.ToInt32(board.BoardId.Substring(1));
                }

                //add the board the dictionary according to emailOfCreator (key)
                if (!UsersBoards.ContainsKey(board.EmailOfCreator))
                {
                    this.UsersBoards.Add(board.EmailOfCreator, new Dictionary<string, Board>());
                }
                UsersBoards[board.EmailOfCreator].Add(board.Name, board);
                //load all the data that is in the board
                board.LoadBoard();
                LoadMembers(board);
            }            
            IdGenerator = max+1;
        }

        /// <summary>
        /// Load the memberships of the board
        /// </summary>
        /// <param name="board">the board to load his members</param>
        public void LoadMembers(Board board)
        {
            foreach (string member in board.Members.Keys)
            {
                if (!Membership.ContainsKey(member))
                {
                    Membership.Add(member, new List<Board>());
                }
                Membership[member].Add(board);
            }
        }

        ///<summary>delete all the the tables in the db that contains boards data</summary> 
        public void DeleteBoardsData()
        {
            DeleteAllBoards();
            DeleteAllBoardsMembers();
            DeleteAllColums();
            DeleteAllTasks();
        }

        ///<summary>delete all the Boards</summary>      
        public void DeleteAllBoards()
        {
            (new BoardDalController()).DeleteAllData();
        }

        ///<summary>delete all the Boards Members</summary>
        public void DeleteAllBoardsMembers()
        {
            MemberDalController membersDalController = new MemberDalController();
            membersDalController.DeleteAllData();
        }

        ///<summary>delete all the columns</summary>  
        public void DeleteAllColums()
        {
            ColumnDalController columnDalController = new ColumnDalController();
            columnDalController.DeleteAllData();
        }

        ///<summary>delete all the tasks</summary>  
        public void DeleteAllTasks()
        {
            TaskDalController taskDalController = new TaskDalController();
            taskDalController.DeleteAllData();
        }

        /// <summary>
        /// Adds a board.
        /// </summary>
        /// <param name="email">The email of the creator of the board</param>
        /// <param name="boardName">The board name</param>
        public void AddBoard(string email, string boardName)
        {
            if (!UserController.GetUser(email).LoggedIn)
            {
                log.Error($"user({email}) that is not logged in trying to add board");
                throw new Exception("user is not logged in");
            }
            Board board;
            if (UsersBoards.ContainsKey(email))
            {
                if (UsersBoards[email].ContainsKey(boardName))
                {
                    log.Error("User already has a board with that name...");
                    throw new Exception($"There is already a board with this name({boardName}) under this email({email})...");
                }
                else
                {
                    IdGenerator += 1;
                    board = new Board(IdGenerator, boardName, email);
                    UsersBoards[email].Add(boardName, board);
                    
                }
            }
            else
            {
                IdGenerator += 1;
                board = new Board(IdGenerator, boardName, email);
                UsersBoards.Add(email, new Dictionary<string, Board>());
                UsersBoards[email].Add(boardName, board);                
            }
            AddMembership(email, board);
        }


        /// <summary>
        /// Returns specified Board object.
        /// </summary>
        /// <param name="email">The email of the creator of the board</param>
        /// <param name="boardName">The name of the wanted board</param>
        /// <returns>Returns wanted board</returns>
        public Board GetBoard(string email, string boardName)
        {
            if (boardName is null)
            {
                log.Error("boardName is null");
                throw new ArgumentNullException(nameof(boardName));
            }

            if (email is null)
            {
                log.Error("email is null");
                throw new ArgumentNullException(nameof(email));
            }

            UserController.CheckUserLogged(email);
            if (UsersBoards.ContainsKey(email))
            {
                if (UsersBoards[email].ContainsKey(boardName))
                {
                    return UsersBoards[email][boardName];
                }
                else
                {
                    log.Error($"There is no board with name({boardName}) under this email: {email}...");
                    throw new Exception($"There is no board with name({boardName}) under this email: {email}...");
                }
            }
            else
            {
                log.Error($"There is no boards with this user : {email}...");
                throw new Exception($"There is no boards with this user : {email}...");
            }
        }

        /// <summary>
        /// return specified Board
        /// </summary>
        /// <param name="creatorEmail">the email of the creator of the board</param>
        /// <param name="boardName">the name of the board to return</param>
        /// <param name="userEmail">the email of the user requesting in order to check login</param>
        /// <returns>Board object of the requested board</returns>
        public Board GetBoard(string creatorEmail, string boardName, string userEmail)
        {
            if (boardName is null)
            {
                log.Error("boardName is null");
                throw new ArgumentNullException(nameof(boardName));
            }

            if (creatorEmail is null)
            {
                log.Error("creator email is null");
                throw new ArgumentNullException(nameof(creatorEmail));
            }

            if (userEmail is null)
            {
                log.Error("user email is null");
                throw new ArgumentNullException(nameof(userEmail));
            }

            UserController.CheckUserLogged(userEmail);
            if (UsersBoards.ContainsKey(creatorEmail))
            {
                if (UsersBoards[creatorEmail].ContainsKey(boardName))
                {
                    return UsersBoards[creatorEmail][boardName];
                }
                else
                {
                    log.Error($"There is no board with name({boardName}) under this email: {creatorEmail}...");
                    throw new Exception($"There is no board with name({boardName}) under this email: {creatorEmail}...");
                }
            }
            else
            {
                log.Error($"There is no boards with this user : {creatorEmail}...");
                throw new Exception($"There is no boards with this user : {creatorEmail}...");
            }
        }

        /// <summary>
        /// add user as a member to a board, the user joins the board.
        /// </summary>
        /// <param name="userEmail">the email of the user to add as a member</param>
        /// <param name="creatorEmail">the email of the creator of the board</param>
        /// <param name="boardName">the name of the board</param>
        public void JoinBoard(string userEmail, string creatorEmail, string boardName)
        {
            AddMembership(userEmail, GetBoard(creatorEmail, boardName, userEmail));
            GetBoard(creatorEmail, boardName, userEmail).Join(userEmail);
        }

        /// <summary>
        /// Remove a board.
        /// </summary>
        /// <param name="creatorEmail">The email of the creator of the board to remove</param>
        /// <param name="userEmail">The email of the user who is requesting the removal</param>
        /// <param name="name">The name of the board to remove</param>
        public void RemoveBoard(string creatorEmail, string userEmail, string name)
        {
            if (!GetBoard(creatorEmail, name, userEmail).Members.ContainsKey(userEmail))
            {
                log.Error($"user({userEmail}) is not a member of the board to remove, hence cant remove board");
                throw new Exception($"user({userEmail}) is not a member of the board to remove, hence cant remove board");
            }
            if (UsersBoards.ContainsKey(creatorEmail))
            {
                if (UsersBoards[creatorEmail].ContainsKey(name))
                {
                    Board toRemove = GetBoard(creatorEmail, name);
                    Dictionary<string, Member> members = toRemove.Members;
                    foreach (string memberEmail in members.Keys)
                    {
                        RemoveMembership(memberEmail, toRemove);
                    }
                    GetBoard(creatorEmail, name).Delete();
                    UsersBoards[creatorEmail].Remove(name);
                }
                else
                {
                    log.Error($"There is no board with this name({name}) under this email:{creatorEmail}...");
                    throw new Exception($"There is no board with this name({name}) under this email: {creatorEmail}...");
                }
            }
            else
            {
                log.Error($"There is no board with this email: {creatorEmail}...");
                throw new Exception($"There is no board with this email: {creatorEmail}...");
            }
        }


        /// <summary>
        /// Returns a list of user's boards
        /// </summary>
        /// <param name="email">the email we want the board from</param>
        /// <returns>The list of all boards of user</returns>
        public List<Board> GetMyBoards(string email)
        {
            if (email == null)
            {
                log.Error("email is null");
                throw new Exception("email is null...");
            }
            UserController.CheckUserLogged(email);

            if (Membership.ContainsKey(email))
            {
                if (Membership[email].Count == 0)
                {
                    return new List<Board>();
                }
                else
                {
                    return Membership[email];
                }
            }
            else
            {
                return new List<Board>();
            }
        }

        /// <summary>
        /// Returns a list of the boards that the user didn't created or joined
        /// </summary>
        /// <param name="email">the email we want the board from</param>
        /// <returns>The list of all boards that the user didn't created or joined</returns>
        public List<Board> GetAllBoards(string email)
        {
            if (email == null)
            {
                log.Error("email is null");
                throw new Exception("email is null...");
            }
            UserController.CheckUserLogged(email);
            List<Board> boards = new List<Board>();
            foreach (string creatorEmail in UsersBoards.Keys)
                    {
                        if (creatorEmail != email)
                        {
                            foreach (Board b in UsersBoards[creatorEmail].Values)
                            {
                                if(!b.Members.ContainsKey(email))
                                    boards.Add(b);
                            }
                        }
                    }
                    return boards;
        }


        /// <summary> 
        /// Returns all the user's tasks that are in progress column.
        /// </summary>
        /// <param name="email">the email we want the task from</param>
        /// <returns>The list of all tasks in progress of user</returns>
        public IList<Task> GetMyInProgress(string email)
        {
            if (email == null)
            {
                log.Error("email is null");
                throw new Exception("email is null...");
            }

            UserController.CheckUserLogged(email);
            
            List<Task> InProgress = new List<Task>();
            foreach (Board board in GetMyBoards(email))
            {
                foreach (Task task in board.GetColumn(email, 1).Tasks.Values)
                {
                    if (String.Equals(task.Assignee, email))
                        InProgress.Add(task);
                }
            }

            return InProgress;
        }
    }
}
