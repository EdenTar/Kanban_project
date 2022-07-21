using IntroSE.Kanban.Frontend.Model;
using IntroSE.Kanban.Frontend.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Frontend.ViewModel
{
    class TaskVM: NotifiableObject
    {
        private TaskController taskController;
        public TaskController TaskController
        {
            get => taskController;
            set
            {
                taskController = value;
                RaisePropertyChanged("id");
            }
        }

        private TaskM task;
        public TaskM TaskM { get => task; set { task = value; } }


        private int id;
        public int Id { get => id; 
            set {
                id = value;
                RaisePropertyChanged("id");
            } 
        }

        private string title;
        public string Title { get => title; 
            set {
                title = value;
                RaisePropertyChanged("title");
            } }
      

        private string description;
        public string Description { get => description;
            set {

                description = value;
                RaisePropertyChanged("description");
            } }
      

        private DateTime dueDate;
        public DateTime DueDate { get => dueDate;
            set {
                dueDate = value;
                RaisePropertyChanged("dueDate");
            } }
       
        private string assignee;
        public string Assignee
        { get => assignee;
            set {
                assignee = value;
                RaisePropertyChanged("assignne");
            } }

       

        private DateTime creationTime;
        public DateTime CreationTime { get => creationTime;
            set {
                creationTime = value;
                RaisePropertyChanged("creationTime");
            } }

        private string message;
        public string Message
        {
            get => message;
            set
            {
                message = value;
                RaisePropertyChanged("massage");
            }
        }


        public TaskVM(TaskM task)
        {
            TaskController = new TaskController(task.Controller.Service);
            this.task = task;
            Id = task.Id;
            Title = task.Title;
            Description = task.Description;
            DueDate = task.DueDate;
            Assignee = task.Assignee;
            CreationTime = task.CreationTime;
        }
        /// <summary>
        /// update task title
        /// </summary>
        public void UpdateTitle()
        {
            try
            {
                TaskController.UpdateTitle(task.UserEmail, task.CreatorEmail, task.BoardName, task.ColumnOrdinal, id, Title);
                task.Title = Title;
                RaisePropertyChanged("title");
            }
            catch (Exception e)
            {
                new PopupErrorV(e.Message).Show();
            }
        }

        /// <summary>
        ///  update task description
        /// </summary>
        public void UpdateDescription()
        {
            try
            {
                TaskController.UpdateDescription(task.UserEmail, task.CreatorEmail, task.BoardName, task.ColumnOrdinal, id, Description);
                task.Description = Description;
                RaisePropertyChanged("description");
            }
            catch (Exception e)
            {
                new PopupErrorV(e.Message).Show();
            }
        }
        /// <summary>
        ///  update task due date
        /// </summary>
        public void UpdateDueDate()
        {
            try
            {
                TaskController.UpdateDueDate(task.UserEmail, task.CreatorEmail, task.BoardName, task.ColumnOrdinal, id, DueDate);
                task.DueDate = DueDate;
                RaisePropertyChanged("dueDate");

            }
            catch (Exception e)
            {
                new PopupErrorV(e.Message).Show();
            }
        }
        /// <summary>
        /// assign task to a board user
        /// </summary>
        public void AssignTask()
        {
            try
            {
                TaskController.AssignTask(task.UserEmail, task.CreatorEmail, task.BoardName, task.ColumnOrdinal, id, Assignee);
                task.Assignee = Assignee;
                RaisePropertyChanged("assignne");
            }
            catch (Exception e)
            {
                Assignee = task.Assignee;
                new PopupErrorV(e.Message).Show();
            }
        }

        /// <summary>
        /// advance task to the next column
        /// </summary>
        public void AdvanceTask()
        {
            try
            {
                (taskController).AdvanceTask(task.UserEmail, task.CreatorEmail, task.BoardName, task.ColumnOrdinal, task.Id);
                task.ColumnM.Tasks.Remove(TaskM);
                task.ColumnM = task.ColumnM.BoardM.Columns.Where((x) => x.Ordinal == (task.ColumnM.Ordinal + 1)).FirstOrDefault();
                task.ColumnOrdinal++;
                task.ColumnM.Tasks.Add(TaskM);
            }
            catch(Exception e)
            {
                new PopupErrorV(e.Message).Show();
            }
        }

        public void DueDateMessage()
        {
            if (DueDate.CompareTo(DateTime.Now) < 0)
                Message = "This task is passed the dueDate";
           
        }
      


    }
}
