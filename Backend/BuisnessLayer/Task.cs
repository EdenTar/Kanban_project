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
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Tests")]
namespace IntroSE.Kanban.Backend.BuisnessLayer
{
    internal class Task : ITask
    {
        private const int MAX_AMOUNT_OF_CHARCTERS_IN_DESCRIPTION = 300;
        private const int MAX_AMOUNT_OF_CHARCTERS_IN_TITLE = 50;
        
        private int id;
        public int Id { get => id; set{ id = value; } }
                                    
        private DateTime creationTime;
        public DateTime CreationTime { get=>creationTime; set {creationTime = value; } }

        private string title;
        public string Title 
        {
            get => title; 
            set
            {
                if (value is null)
                {
                    log.Error("A user attempted to update the title with null value");
                    throw new Exception("Title cant be null ");
                }
                if (value == "")
                {
                    log.Error("A user attempted to update the title with an empty value");
                    throw new Exception("Title cannot be empty");
                }
                if (value.Length > MAX_AMOUNT_OF_CHARCTERS_IN_TITLE)
                {
                    log.Error("A user attempted to update the title with a title that is longer than 50");
                    throw new Exception("The title is too long");
                }
                this.title = value;
                this.TaskDTO.Title = value;
            }
        }

        private string description ;
        public string Description
        {
            get => description; 
            set 
            {
                if (value.Length > MAX_AMOUNT_OF_CHARCTERS_IN_DESCRIPTION)
                {
                    log.Error("A user attempted to update the Description with a Description that is longer than 300");
                    throw new Exception("The Description is too long");
                }
                this.description = value;
                this.TaskDTO.Description = value;
            } 
        }

        private DateTime dueDate;
        public DateTime DueDate 
        {
            get => dueDate; 
            set
            {
                if (DateTime.Compare(value, DateTime.Now) < 0)
                {
                    log.Error("A user attempted to update due date to a time that has already passed");
                    throw new Exception("The due date cant be earlier than our current date");
                }
                this.dueDate = value;
                this.TaskDTO.DueDate=value;
            } 
        }

        private string assignee;
        public string Assignee 
        { 
            get => assignee; 
            set 
            { 
                if (value==null)
                {
                    log.Error("There was an attempt assign a null assignee");
                    throw new Exception("assignee cant be null ");
                }
                assignee = value;
                this.TaskDTO.Assignee = value;
            } 
        }

        private TaskDTO taskDTO;
        public TaskDTO TaskDTO { get=>taskDTO; set {taskDTO = value; } }

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        /// <summary>
        /// Initialize The Task object.
        /// </summary>
        /// <param name="Id">The I.D of the task</param>
        /// <param name="CreationTime">The creation time of the task</param>
        /// <param name="Title">The title of the task</param>
        /// <param name="Description">The description of the task</param>
        /// <param name="DueDate">The due date of the task</param>
        public Task(int taskId,string columnId, string title, string description, DateTime creationTime, DateTime dueDate,string assignee)
        {
            string TaskDTOid = columnId + "T" + taskId.ToString();
            TaskDTO = new TaskDTO(TaskDTOid, taskId,columnId, 0, title, description, creationTime, dueDate, assignee);
            Id = taskId;
            CreationTime = creationTime;
            Title = title;
            DueDate = dueDate;
            Assignee = assignee;
            Description = description;
            TaskDTO.InsertTask();
            
        }

        /// <summary>
        /// initialize Task object using data layer object.
        /// </summary>
        /// <param name="taskDTOCopy">the data object to create task with.</param>
        public Task(TaskDTO taskDTOCopy)
        {
            this.taskDTO = taskDTOCopy;
            this.id = taskDTOCopy.TaskId;
            this.creationTime = taskDTOCopy.CreationTime;
            this.description = taskDTOCopy.Description;
            this.title = taskDTOCopy.Title;
            this.dueDate = taskDTOCopy.DueDate;
            this.assignee = taskDTOCopy.Assignee;
            this.taskDTO.Persisted = true;
        }

        /// <summary>
        /// delete the task's persisted data.
        /// </summary>
        public void Delete() 
        {
            TaskDTO.Delete();
        }

        /// <summary>
        /// checks if given assignee is the same as the task assignee
        /// </summary>
        /// <param name="assignee">the email to check if assignee</param>
        public void CheckAssignee(string userEmail) 
        {
            if (!Assignee.Equals(userEmail))
            {
                log.Error("There was an attempt to update the task dueDate by someone that is not assigne to this task");
                throw new Exception("Only the assignee can update the task");
            }
        }

        public void ChangeColumn(IColumn column) 
        {
            TaskDTO.ColumnOrdinal = column.NumberOfColumn;
            TaskDTO.ColumnId = column.ColumnId;
            TaskDTO.Id = TaskDTO.ColumnId + "T" + Id.ToString();
        }
    }
}
