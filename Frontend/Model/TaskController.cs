using IntroSE.Kanban.Frontend.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.ServiceLayer;
using System.Collections.ObjectModel;
using Task = IntroSE.Kanban.Backend.ServiceLayer.Task;

namespace IntroSE.Kanban.Frontend.Model
{
    internal class TaskController: BackendController
    {
      
        public TaskController(Service service):base(service)
        {

        }
        /// <summary>
        /// Turns to the service in order to add a task.
        /// </summary>
        /// <param name="columnM">the column model</param>
        /// <param name="title">Title of the new task</param>
        /// <param name="description">Description of the new task</param>
        /// <param name="dueDate">The due date if the new task</param>
        /// <returns>Returns a new Model object</returns>
        public TaskM AddTask(ColumnM columnM, string title, string description, DateTime dueDate)
        {
            Response<Task> task = Service.AddTask(columnM.UserEmail , columnM.CreatorEmail, columnM.BoardName,title,description,dueDate);
            if (task.ErrorOccured)
            {
                throw new Exception(task.ErrorMessage);
            }
            return new TaskM(this, task.Value.Id, title, description, columnM.UserEmail, dueDate, task.Value.CreationTime, columnM);
        }
        /// <summary>
        /// Turns to the service in order to update a task title.
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="title">New title for the task</param>
        public void UpdateTitle(string userEmail, string creatorEmail, String boardName,int columnOrdinal, int taskId, string title )
        {
            Response Task = Service.UpdateTaskTitle(userEmail, creatorEmail, boardName, columnOrdinal, taskId, title);
            if (Task.ErrorOccured)
            {
                throw new Exception(Task.ErrorMessage);
            }
          
        }
        /// <summary>
        /// Turns to the service in order to update a task description.
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="description">New description for the task</param>
        public void UpdateDescription(string userEmail, string creatorEmail, String boardName, int columnOrdinal, int taskId, string description)
        {
            Response Task = Service.UpdateTaskDescription(userEmail, creatorEmail, boardName, columnOrdinal, taskId, description);
            if (Task.ErrorOccured)
            {
                throw new Exception(Task.ErrorMessage);
            }
           
        }
        /// <summary>
        /// Turns to the service in order to update a task dueDate.
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="dueDate">The new due date of the column</param>
        public void UpdateDueDate(string userEmail, string creatorEmail, String boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {
            Response Task = Service.UpdateTaskDueDate(userEmail, creatorEmail, boardName, columnOrdinal, taskId, dueDate);
            if (Task.ErrorOccured)
            {
                throw new Exception(Task.ErrorMessage);
            }
            
        }
        /// <summary>
        /// Turns to the service in order to assigne a task another user.
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>        
        /// <param name="newAssignee">Email of the user to assign to task to</param>
        public void AssignTask(string userEmail, string creatorEmail, String boardName, int columnOrdinal, int taskId, string newAssignee)
        {
            Response Task = Service.AssignTask(userEmail, creatorEmail, boardName, columnOrdinal, taskId, newAssignee);
            if (Task.ErrorOccured)
            {
                throw new Exception(Task.ErrorMessage);
            }
        }

        /// <summary>
        /// Advance a task to the next column
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        public void AdvanceTask(string userEmail, string creatorEmail, String boardName, int columnOrdinal, int taskId)
        {
            Response Task = Service.AdvanceTask(userEmail, creatorEmail, boardName, columnOrdinal, taskId);
            if (Task.ErrorOccured)
            {
                throw new Exception(Task.ErrorMessage);
            }
        }

        /// <summary>
        /// Gets all InProgressTasks and from bussines layer and convert it to ObservableCollection<TaskM> 
        /// </summary>
        /// <param name="userM"></param>
        /// <returns>Collection of TaskM</returns>
        public ObservableCollection<TaskM> InProgressTasks(UserM userM)
        {
            Response<IList<Task>> tasks = Service.InProgressTasks(userM.Email);
            if (tasks.ErrorOccured)
            {
                throw new Exception(tasks.ErrorMessage);
            }
            return new ObservableCollection<TaskM>( tasks.Value.Select((t)=>new TaskM(this,t.Id,t.Title,t.Description,t.emailAssignee,t.DueDate,t.CreationTime)));
        }


    }
}
