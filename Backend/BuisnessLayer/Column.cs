using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using System.Reflection;
using System.IO;
using IntroSE.Kanban.DataAccessLayer.DTOs;
using IntroSE.Kanban.DataAccessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Tests")]
namespace IntroSE.Kanban.Backend.BuisnessLayer
{
    internal class Column : IColumn
    {
        private const int INFINITY_TASKS = -1;

        private ColumnDTO columnDTO;
        public ColumnDTO ColumnDTO { get=> columnDTO; set { columnDTO = value; } }
        
        private string columnId;
        public string ColumnId 
        { 
            get => columnId;
            set 
            { 
                columnId = value;
                ColumnDTO.Id = columnId;
            } 
        }

        private int numberOfTasks;
        public int NumberOfTasks { get => numberOfTasks; set{ numberOfTasks = value; } }
        
        private int maxNumOfTasks;
        public int MaxNumOfTasks
        {
            get => maxNumOfTasks;
            set
            {
                if (value < INFINITY_TASKS)
                {
                    log.Error($"The new limit({value}) can't be below -1(infinity)");
                    throw new ArgumentException("Invalid input");
                }
                if (value < NumberOfTasks && value != INFINITY_TASKS)
                {
                    log.Error($"The new limit({value}) can't be below the amount of currnet tasks({this.numberOfTasks})...");
                    throw new ArgumentException("Already have more task then the limit...");
                }
                maxNumOfTasks = value;
                ColumnDTO.Limit = MaxNumOfTasks;
            }
        }
        
        private int numberOfColumn;
        public int NumberOfColumn
        {
            get => numberOfColumn;
            set
            {
                numberOfColumn = value;
                ColumnDTO.ColumnOrdinal = numberOfColumn;
                ColumnId = ColumnDTO.BoardId + "C" + numberOfColumn;
                foreach (Task task in Tasks.Values) 
                {
                    task.ChangeColumn(this);
                }
            }
        }
        
        private string name;
        public string Name
        {
            get => name;
            set 
            {
                name = value;
                columnDTO.Name = name;
            }    
        }

        private Dictionary<int, ITask> tasks = new Dictionary<int, ITask>();
        public Dictionary<int, ITask> Tasks { get => tasks; set { tasks = value; } }

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        /// <summary>
        /// Initialize an emptyColumn object.
        /// where the max amount of tasks is unlimited.
        /// </summary>
        /// <param name="numberOfColumn"></param>
        public Column(string boardID, int numberOfColumn, string name)
        {
            ColumnDTO = new ColumnDTO(boardID+"C"+numberOfColumn.ToString(),numberOfColumn,INFINITY_TASKS, boardID, name);
            Tasks = new Dictionary<int, ITask>();
            NumberOfColumn = numberOfColumn;
            ColumnId = boardID + "C" + numberOfColumn.ToString(); 
            MaxNumOfTasks = INFINITY_TASKS;
            NumberOfTasks = 0;
            Name=name;
            ColumnDTO.InsertColumn();
        }

        /// <summary>
        /// Initialize an Column object (copy of ColumDTO).
        /// </summary>
        /// <param columnDTOcopy="columnDTOcopy"></param>
        public Column(ColumnDTO columnDTOcopy)
        {
            this.columnDTO = columnDTOcopy;
            this.columnId = columnDTOcopy.Id;
            this.maxNumOfTasks = columnDTOcopy.Limit;
            this.numberOfColumn = columnDTOcopy.ColumnOrdinal;
            this.tasks = new Dictionary<int, ITask>();
            this.name = columnDTOcopy.Name;
        }

        ///<summary>load all the tasks of the column in the db and insert them to the dictionary</summary> 
        ////// <returns>the larges task ID</returns>
        public int LoadTasksInColumn()
        {
            int maxId = 0;
            NumberOfTasks = 0;
            //get all the tasks of the specific column from the db
            List<TaskDTO> tasksList = new TaskDalController().LoadTasks(ColumnId);
            foreach (TaskDTO task in tasksList)
            {
                //create a new task object (copy of TaskDTO) and add it to the dictionary
                Task t = new Task(task);
                Tasks.Add(t.Id,t);
                if (t.Id >= maxId)
                    maxId = t.Id;

            }
            NumberOfTasks = tasksList.Count;
            return maxId;
        }

        /// <summary>
        /// Returns a specific task.
        /// </summary>
        /// <param name="taskId">the id of the task</param>
        /// <returns>Task object , with the taskId </returns>
        public ITask GetTask(int taskId)
        {
            if (!Tasks.ContainsKey(taskId))
            {
                log.Error("There was an attempt to recive an non-existing task");
                throw new Exception("This task does not exist in the column");
            }
            return Tasks[taskId];
        }

        /// <summary>
        /// Adds a task to the column.
        /// </summary>
        /// <param name="task">The task to insert to the column</param>
        public void AddTask(ITask task) 
        {
            if (task == null)
            {
                log.Error("There was an attempt to insert a null task. ");
                throw new ArgumentNullException("Task cannot be null...");
            }
            else if (IsFull())
            {
                log.Error("There was an attempt to insert a task to a full column. ");
                throw new Exception("The column is already full...");
            }
            else
            {
                log.Debug("The task was added successfully");
                Tasks.Add(task.Id, task);
                NumberOfTasks++;
            }
        }


        /// <summary>
        /// Removes task from the column
        /// </summary>
        /// <param name="taskId"></param>
        public void RemoveTask(int taskId)
        {
            if (!Tasks.ContainsKey(taskId))
            {
                log.Error("There was an attempt to remove a non-existing task.");
                throw new Exception("Task does not exists, in this column...");
            }
            else
            {
                Tasks.Remove(taskId);
                NumberOfTasks--;
                log.Debug("The task was removed successfully");
            }
        }


        /// <summary>
        /// returns if the Column is full.
        /// </summary>
        /// <returns>true if column is full, false otherwise</returns>
        public bool IsFull()
        { return (MaxNumOfTasks != INFINITY_TASKS) && (NumberOfTasks >= MaxNumOfTasks); }

        /// <summary>
        /// delete the column from the presisted data, and delete all the tasks of the column.
        /// </summary>
        public void Delete() 
        {
            ColumnDTO.Delete();
            foreach (ITask task in Tasks.Values) 
            {
                task.Delete();
            }
        }

        /// <summary>
        /// Removes a column onto another column by transferring all the tasks.
        /// </summary>
        /// <param name="column">the column to remove to</param>
        public void RemoveColumnTo(IColumn column)
        {
            if (!(column.MaxNumOfTasks==-1)&& !( column.MaxNumOfTasks > NumberOfTasks + column.NumberOfTasks ) ) 
            {
                log.Error("the amount of tasks exceeds the limit in the new column");
                throw new Exception("the amount of tasks exceeds the limit in the new column");
            }
            else
            {
                foreach (ITask task in Tasks.Values)
                {
                    column.AddTask(task);
                    RemoveTask(task.Id);
                }
                Delete();
            }
        }
    }
}
