using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using System.IO;
using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using IntroSE.Kanban.Backend.DataAccessLayer;

namespace IntroSE.Kanban.Backend.BuisnessLayer

{
    class Board
    {
        private const int ORDINAL_NUM_FIRST_COLUMN = 0;
        private int lastOrdinal;
        public int LastOrdinal 
        {
            get => lastOrdinal;
            set 
            {
                if (value >= ORDINAL_NUM_FIRST_COLUMN)
                {
                    lastOrdinal = value;
                }
                else 
                {
                    log.Error("can't change the value of the last ordinal to number that is not above the first ordinal");
                    throw new Exception("can't change the value of the last ordinal to number that is not above the first ordinal");
                }
            }
        }
        private string boardId;
        public string BoardId 
        {
            get=>boardId; 
            set {
                boardId = "B" + value;
            }
        }
        
        private string name;
        public string Name { get=>name; set {name = value; } }
        
        private string emailOfCreator;
        public string EmailOfCreator { get=>emailOfCreator; set { emailOfCreator = value; } }
        
        private int totalTaskNum;
        public int TotalTaskNum
        { 
            get => totalTaskNum;
            set 
            {
                totalTaskNum = value;
                BoardDTO.TotalNumOfTasks = TotalTaskNum;
            } 
        }
        private List<IColumn> columns;      
        public List<IColumn> Columns
        { 
            get=>columns;
            set
            {
                columns = value;
            } 
        }
        
        private Dictionary<string, Member> members;
        public Dictionary<string, Member> Members { get=> members; set { members = value; } }

        private BoardDTO boardDTO;
        public BoardDTO BoardDTO { get=> boardDTO; set { boardDTO = value; } }

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary> Initialize Board object.</summary>
        /// <param name="id">the unique id of the board</param>
        /// <param name="name">The name of the board</param>
        /// <param name="email">The email of the creator of the board</param>
        public Board(long id, string name, string email)
        {
            _ = name ?? throw new ArgumentNullException(nameof(name));
            _ = email ?? throw new ArgumentNullException(nameof(email));

            BoardDTO = new BoardDTO("B"+id.ToString(),email, name, 0);
            Members = new Dictionary<string, Member>();
            Name = name;
            EmailOfCreator = email;
            TotalTaskNum = 0;
            BoardId = id.ToString();
            Columns = new List<IColumn>();
            columns.Add(new Column(BoardId, 0, "backlog"));
            columns.Add(new Column(BoardId, 1, "in progress"));
            columns.Add(new Column(BoardId, 2, "done"));
            LastOrdinal = 2;
            BoardDTO.Insert();
            Members.Add(EmailOfCreator,new Member(EmailOfCreator, BoardId));
            
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }

        /// <summary> Initialize Board object using data layer object.</summary>
        /// <param boardDTOcopy="boardDTOcopy">the data object of the board</param>
        public Board(BoardDTO boardDTOcopy)
        {
            boardDTO = boardDTOcopy;
            name = boardDTOcopy.BoardName;
            boardId = boardDTOcopy.Id;
            emailOfCreator = boardDTOcopy.EmailOfCreator;
            totalTaskNum = boardDTOcopy.TotalNumOfTasks;
            columns = new List<IColumn>();
            Members = new Dictionary<string, Member>();
            LastOrdinal = columns.Count();
        }

        ///<summary>
        ///load all the columns and members of the board from the db
        ///</summary>      
        public void LoadBoard()
        {
            TotalTaskNum = 0;
            //get all the columns of the board from the db
            List<ColumnDTO> columnList = (new ColumnDalController()).LoadAllColumns(BoardId);
            columnList.Sort();
            foreach (ColumnDTO columnDTO in columnList)
            {
                columnDTO.Persisted = true;
                //create Column object for each ColumDTO and insert it to the columns array
                Column column = new Column(columnDTO);
                Columns.Add(column);
                //load all the tasks of the column
                int id=column.LoadTasksInColumn();
                if (id > TotalTaskNum)
                    TotalTaskNum = id;
            }
            LastOrdinal = Columns.Count;
            TotalTaskNum++;
            //load all boardMembers according to this board
            foreach (MemberDTO memberDTO in (new MemberDalController()).LoadBoardMembers(BoardId))
            {
                Members.Add(memberDTO.Email, new Member(memberDTO));
            }
        }


        /// <summary>
        /// Addes a new task to the board
        /// </summary>
        /// <param name="title">The title of the task</param>
        /// <param name="description">The description of the task</param>
        /// <param name="dueDate">The due date of the task</param>
        /// <param name="assignee">The assignee of the task</param>
        /// <returns>The new Task object added to board</returns>
        public Task AddTask(string title, string description, DateTime dueDate, string assignee)
        {
            _ = title ?? throw new ArgumentNullException(nameof(title));
            _ = description ?? throw new ArgumentNullException(nameof(description));
            _ = assignee ?? throw new ArgumentNullException(nameof(assignee));
            
            if(GetColumn(assignee,ORDINAL_NUM_FIRST_COLUMN).IsFull())
                throw new Exception("The first column is full");
            string columnId = BoardId + "C0";
            Task task = new Task(TotalTaskNum+1, columnId, title, description, DateTime.Now, dueDate, assignee);
            GetColumn(assignee,ORDINAL_NUM_FIRST_COLUMN).AddTask(task);
            TotalTaskNum++;
            return task;
        }

        /// <summary>        
        /// Get the limit of a column.
        /// </summary>
        /// <param name="columnOrdinal">The ordinal number of the wanted column</param>
        /// <returns>The limit of the column.</returns>
        public IColumn GetColumn(string userEmail, int columnOrdinal)
        {
            CheckMember(userEmail);
            if ((Columns.Count < columnOrdinal) || (columnOrdinal < ORDINAL_NUM_FIRST_COLUMN))
            {
                log.Error($"The ordinal number must be a number between {ORDINAL_NUM_FIRST_COLUMN} and {LastOrdinal} included");
                throw new Exception($"The ordinal number must be a number between {ORDINAL_NUM_FIRST_COLUMN} and {LastOrdinal} included");
            }
            return columns.ElementAt<IColumn>(columnOrdinal);
        }

        public void CheckMember(string userEmail) 
        {
            if (!Members.ContainsKey(userEmail))
            {
                log.Error($"user({userEmail}) is not a member, hence cannot access column");
                throw new Exception($"user({userEmail}) is not a member, hence cannot access column");
            }
        }


        /// <summary>
        /// moves the tasks to the next column
        /// </summary>
        /// <param name="columnOrdinal">the column ordinal of the task in the board</param>
        /// <param name="taskId">task ID</param>
        /// <param name="assignee">the user who is assigned to the task</param>
        public void AdvanceTask(int columnOrdinal, int taskId, string assignee)
        {
            CheckIsColumnLast(columnOrdinal);
            if (!GetColumn(assignee, columnOrdinal).Tasks.ContainsKey(taskId))
            {
                log.Error("A non-exsisting task was attempted to be moved to the next column");
                throw new Exception("No such task in this column");
            }
            if (GetColumn(assignee, columnOrdinal + 1).IsFull())
            {
                log.Error("A task was attempted to be moved to a full column");
                throw new Exception("The next column is full");
            }
            GetColumn(assignee, columnOrdinal).GetTask(taskId).CheckAssignee(assignee);
            
            ITask task = GetColumn(assignee,columnOrdinal).GetTask(taskId);
            GetColumn(assignee, columnOrdinal).RemoveTask(taskId);
            GetColumn(assignee, columnOrdinal +1).AddTask(task);
            task.ChangeColumn(GetColumn(assignee, columnOrdinal + 1)); 
        }

        /// <summary>
        /// delete board from the database
        /// also delete the columns of the board and the members
        /// </summary>
        public void Delete() 
        {
            BoardDTO.Delete();
            foreach (Column c in Columns) 
            {
                c.Delete();
            }
            foreach (Member m in Members.Values) 
            {
                m.Delete();
            }
        }

        /// <summary>
        /// add new member to the board
        /// </summary>
        /// <param name="email">the email of the user to join the board</param>
        public void Join(string email) 
        {
            Members.Add(email, new Member(email, BoardId));
        }

        /// <summary>
        /// Update the title of the task.
        /// </summary>
        /// <param name="columnOrdinal"></param>
        /// <param name="taskId">The id of the task</param>
        /// <param name="title">The title to update</param>
        /// <param name="assignee">The user requesting the update</param>
        public void UpdateTitle(int columnOrdinal, int taskId, string title, string assignee)
        {
            CheckIsColumnLast(columnOrdinal);
            ITask task = GetColumn(assignee, columnOrdinal).GetTask(taskId);
            task.CheckAssignee(assignee);
            task.Title = title;
        }

        /// <summary>
        /// Update the due date of the task.
        /// </summary>
        /// <param name="columnOrdinal"></param>
        /// <param name="taskId">The if of the task to update</param>
        /// <param name="dueDate">The due date to change to</param>
        /// <param name="assignee">the user requesting the update</param>
        public void UpdateDueDate(int columnOrdinal, int taskId, DateTime dueDate, string assignee)
        {
            CheckIsColumnLast(columnOrdinal);
            ITask task = GetColumn(assignee, columnOrdinal).GetTask(taskId);
            task.CheckAssignee(assignee);
            task.DueDate = dueDate;
        }

        /// <summary>
        /// Update the description of the task.
        /// </summary>
        /// <param name="taskId">The id of the task to update</param>
        /// <param name="description">The description to change to</param>
        /// <param name="assignee">The user requesting the update</param>
        public void UpdateDescription(int columnOrdinal, int taskId, string description, string assignee)
        {
            CheckIsColumnLast(columnOrdinal);
            ITask task = GetColumn(assignee, columnOrdinal).GetTask(taskId);
            task.CheckAssignee(assignee);
            task.Description = description;
        }

        /// <summary>
        /// Assigns a task to a user
        /// </summary>
        /// <param name="columnOrdinal"></param>
        /// <param name="taskId">The task ID</param>
        /// <param name="assignee">The user that is going to be assign to the task</param>
        public void AssignTask(int taskId, int columnOrdinal, string assignee)
        {
            CheckIsColumnLast(columnOrdinal);
            GetColumn(assignee, columnOrdinal).GetTask(taskId).Assignee = assignee;
        }

        /// <summary>
        /// Checks if the column ordinal is the ordinal of the last column, if it's not throws an exception.
        /// </summary>
        /// <param name="columnOrdinal">the column ordinal to check.</param>
        public void CheckIsColumnLast(int columnOrdinal) 
        {
            if (columnOrdinal == LastOrdinal)
            {
                log.Error("There was an attempt to update a DONE task.");
                throw new Exception("Cannot update task while in done column...");
            }
        }

        /// <summary>
        /// Adds column to the board.
        /// </summary>
        /// <param name="userEmail">the user trying to add the column. must be logged in and a member of the board.</param>
        /// <param name="columnOrdinal">the ordinal location to add the column to.</param>
        /// <param name="columnName">the name of the new column.</param>
        public void AddColumn(string userEmail, int columnOrdinal, string columnName) 
        {
            if (userEmail == null) 
                throw new ArgumentNullException(nameof(userEmail));
            if (columnName == null) 
                throw new ArgumentNullException(nameof(columnName));

            CheckMember(userEmail);
            for (int i = Columns.Count-1; i >= columnOrdinal; i--) 
            {
                Columns[i].NumberOfColumn+=1;
            }
            Columns.Insert(columnOrdinal, new Column(BoardId, columnOrdinal, columnName));
            LastOrdinal += 1;
        }

        /// <summary>
        /// remove column from board.
        /// </summary>
        /// <param name="userEmail">the email of the user trying to remove the column, needed to be logged in and a member of the board.</param>
        /// <param name="columnOrdinal">the ordinal number of the column to remove.</param>
        public void RemoveColumn(string userEmail, int columnOrdinal) 
        {
            if (Columns.Count <= 2) 
            {
                throw new Exception("can't have less then 2 columns");
            }
            CheckMember(userEmail);
            IColumn columnToRemove = GetColumn(userEmail, columnOrdinal);
            IColumn columnToRemoveTo;
            if (columnOrdinal == ORDINAL_NUM_FIRST_COLUMN)
            {
                columnToRemoveTo = GetColumn(userEmail, columnOrdinal + 1);
            }
            else 
            {
                columnToRemoveTo = GetColumn(userEmail,columnOrdinal-1);
            }
            columnToRemove.RemoveColumnTo(columnToRemoveTo);
            Columns.Remove(columnToRemove);
            int i = 0;
            foreach(Column c in Columns) 
            {
                c.NumberOfColumn = i;
                i++;
            }
            LastOrdinal--;
        }

        /// <summary>
        /// Move the Column.
        /// </summary>
        /// <param name="userEmail">the user trying to move the column, must be logged in and a member of the board.</param>
        /// <param name="columnOrdinal">the column ordinal of the column to move.</param>
        /// <param name="shiftSize">the amount to shift the column to the right, if minus then to the left.</param>
        public void MoveColumn(string userEmail, int columnOrdinal, int shiftSize) 
        {
            CheckMember(userEmail);
            IColumn columnToMove = GetColumn(userEmail, columnOrdinal);
            if (columnToMove.MaxNumOfTasks > 0)
                throw new Exception("can't move column with tasks");
            columnToMove.ColumnDTO.Delete();
            Columns.RemoveAt(columnOrdinal);

            if ( (shiftSize + columnOrdinal > Columns.Count+1) || (columnOrdinal + shiftSize < 0) ) 
            {
                log.Error("tried to move the column out of the board");
                throw new Exception("can't shift the column out of the board");
            }
            if (shiftSize >= 0)
            {
                foreach (Column c in Columns.GetRange(columnOrdinal,shiftSize))
                {
                    c.NumberOfColumn--;
                }
            }
            else 
            {
                List<IColumn> reverse = Columns.GetRange(columnOrdinal + shiftSize, -shiftSize);
                reverse.Reverse();
                foreach (Column c in reverse) 
                {
                    c.NumberOfColumn++;
                }
            }
            columnToMove.ColumnDTO.Persisted = false;
            columnToMove.NumberOfColumn = columnOrdinal + shiftSize;
            Columns.Insert(columnOrdinal + shiftSize, columnToMove);
            columnToMove.ColumnDTO.InsertColumn();
        }
    }
}
