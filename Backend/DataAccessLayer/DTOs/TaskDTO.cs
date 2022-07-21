using IntroSE.Kanban.Backend.DataAccessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using System;

namespace IntroSE.Kanban.DataAccessLayer.DTOs
{
    internal class TaskDTO : DTO
    {
        public const string TaskIdColumnName = "id";
        public const string TaskTaskIdColumnName = "taskId";
        public const string TaskColumnIdColumnName = "columnId";
        public const string TaskColumnOrdinalColumnName = "columnOrdinal";
        public const string TaskTitleColumnName = "title";
        public const string TaskDescriptionColumnName = "description";
        public const string TaskCreationTimeColumnName = "creationTime";
        public const string TaskDueDateColumnName = "dueDate";
        public const string TaskAssigneeColumnName = "assignee";

        private Boolean persisted;
        public Boolean Persisted { get => persisted; set { persisted = value; } }

        private string id;
        public override string Id
        {
            get => id;
            set
            {       
                if (Persisted)
                {
                    _controller.Update(TaskIdColumnName, Id, TaskIdColumnName, value);
                }
                id = value;
            }
        }

        private int taskId;
        public int TaskId { get => taskId; set { taskId = value; } }
       
        private string columnId;
        public string ColumnId 
        {
            get => columnId;
            set
            {
                columnId = value;
                if (Persisted)
                    _controller.Update(TaskIdColumnName, Id, TaskColumnIdColumnName, value); 
            }
        }
     
        private int columnOrdinal;
        public int ColumnOrdinal 
        {
            get => columnOrdinal;
            set
            {
                columnOrdinal = value;
                if (Persisted)
                    _controller.Update(TaskIdColumnName,Id, TaskColumnOrdinalColumnName, value); 
            } 
        }

        private string title;
        public string Title
        { 
            get => title; 
            set 
            {
                title = value; 
                if(Persisted)
                   _controller.Update(TaskIdColumnName,Id, TaskTitleColumnName, value); 
            } 
        }

        private string description;
        public string Description 
        {
            get => description; 
            set
            {
                description = value;
                if (Persisted)
                    _controller.Update(TaskIdColumnName,Id, TaskDescriptionColumnName, value); 
            }
        }
      
        private DateTime creationTime;
        public DateTime CreationTime { get => creationTime; set { creationTime = value; } }

        private DateTime dueDate;
        public DateTime DueDate 
        {
            get => dueDate;
            set 
            { 
                dueDate = value;
                if (Persisted)
                    _controller.Update(TaskIdColumnName,Id, TaskDueDateColumnName, value);
            }
        }

        private string assignee;
        public string Assignee 
        { 
            get => assignee;
            set 
            {
                assignee = value;
                if (Persisted)
                    _controller.Update(TaskIdColumnName,Id, TaskAssigneeColumnName, value); 
            } 
        }

        /// <summary>
        /// initialize TaskDTO object
        /// </summary>
        /// <param name="id">unique id of the task</param>
        /// <param name="taskId">the local board task id</param>
        /// <param name="columnId">the column id</param>
        /// <param name="columnOrdinal">the columnOrdinal</param>
        /// <param name="title">the title of the task</param>
        /// <param name="description">the description of the task</param>
        /// <param name="creationTime">the creation time of the task</param>
        /// <param name="dueDate">the dueDate of the task</param>
        /// <param name="assignee">the assignne of the task</param>
        public TaskDTO(string id,int taskId, string columnId, int columnOrdinal,string title, string description, DateTime creationTime,DateTime dueDate, string assignee) : base(new TaskDalController())
        {
            Persisted = false;
            Id = id;
            TaskId = taskId;
            ColumnId = columnId;
            ColumnOrdinal = columnOrdinal;
            Title = title;
            Description = description;
            CreationTime = creationTime;
            DueDate = dueDate;
            Assignee = assignee;
        }

        /// <summary>
        /// insert the task to persisted data.
        /// </summary>
        public void InsertTask()
        {
            ((TaskDalController)_controller).Insert(this);
            Persisted = true;
        }

        /// <summary>
        /// delete the task from persisted data.
        /// </summary>
        public void Delete() 
        {
            ((TaskDalController)_controller).Delete(TaskIdColumnName, this);
        }
    }
}