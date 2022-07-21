using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BuisnessLayer;
using Buisness = IntroSE.Kanban.Backend.BuisnessLayer;
using log4net;
using log4net.Config;
using System.Reflection;
using System.IO;


namespace IntroSE.Kanban.Backend.ServiceLayer
{
    class TaskService
    {
        private BoardController boardController;

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        /// <summary>
        /// Initialize TaskService object.
        /// </summary>
        /// <param name="boardController">The system board controller</param>
        public TaskService(BoardController boardController)
        {
            this.boardController = boardController;
        }


        /// <summary>
        /// moves the task to the next column
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="creatorEmail">Email of creator of the board.</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <returns></returns>
        public Response AdvanceTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId)
        {
            try
            {
                log.Info("Trying to advance task...");
                boardController.GetBoard(creatorEmail, boardName, userEmail).AdvanceTask(columnOrdinal, taskId, userEmail);
                log.Debug("The task has been adnvanced Sucssesfully");
                return new Response();
            }
            catch (Exception e)
            {
                log.Debug("The task could not be advanced to the next column");
                return new Response(e.Message);
            }
        }


        /// <summary>
        /// Update task title
        /// </summary>
        /// <param name="userEmail">Email of the user. Must be logged in, member of the board, and the assignee of the task</param>
        /// <param name="creatorEmail">Email of creator of the board</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="title">New title for the task</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskTitle(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string title)
        {
            try
            {
                log.Info("Trying to update task title...");
                boardController.GetBoard(creatorEmail, boardName, userEmail).UpdateTitle(columnOrdinal, taskId, title, userEmail);
                log.Debug("Task title has been changed sucssecfully");
                return new Response();
            }
            catch (Exception e)
            {
                log.Debug("Task title did not change successfully ");
                return new Response(e.Message);
            }
        }


        /// <summary>
        /// Update the description of a task
        /// </summary>
        /// <param name="userEmail">Email of the user. Must be logged in, member of the board, and the assignee of the task</param>
        /// <param name="creatorEmail">Email of creator of the board</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="description">New description for the task</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskDescription(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string description)
        {
            try
            {
                log.Info("Trying to update task description...");
                boardController.GetBoard(creatorEmail, boardName, userEmail).UpdateDescription(columnOrdinal, taskId, description, userEmail);
                log.Debug("Task Description has been changed sucssecfully");
                return new Response();
            }
            catch (Exception e)
            {
                log.Debug("Task Description did not change successfully ");
                return new Response(e.Message);
            }
        }


        /// <summary>
        /// Update the due date of a task
        /// </summary>
        /// <param name="userEmail">Email of the user. Must be logged in, member of the board, and the assignee of the task</param>
        /// <param name="creatorEmail">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="dueDate">The new due date of the column</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskDueDate(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {
            try
            {
                log.Info("Trying to update task dueDate...");
                boardController.GetBoard(creatorEmail, boardName, userEmail).UpdateDueDate(columnOrdinal, taskId, dueDate, userEmail);
                log.Debug("Task due date has been changed sucssecfully");
                return new Response();
            }
            catch (Exception e)
            {
                log.Debug("Task due date did not change successfully ");
                return new Response(e.Message);
            }
        }


        /// <summary>
        /// Adds a new task to the board to the "backlog" column.
        /// </summary>
        /// <param name="userEmail">Email of the user. Must be logged in and a member of the board.</param>
        /// <param name="creatorEmail">The email of the creator of the board the task is added to</param>
        /// <param name="boardName">The name of the board the task is added to</param>
        /// <param name="title">The title of the new task</param>
        /// <param name="description">The description of the new task</param>
        /// <param name="dueDate">The due date of the task</param>
        /// <returns>Response object. The response will contain Task instance in case of no error, error message otherwise</returns>
        public Response<Task> AddTask(string userEmail, string creatorEmail, string boardName, string title, string description, DateTime dueDate)
        {
            try
            {
                log.Info("Trying to add new task to the board...");
                Buisness.Task task = boardController.GetBoard(creatorEmail, boardName, userEmail).AddTask(title, description, dueDate, userEmail);
                log.Debug("The task was added succesfully");
                return Response<Task>.FromValue(new Task(task.Id, task.CreationTime, title, description, dueDate, userEmail));
            }
            catch (Exception e)
            {
                log.Debug("Failed to add task...");
                return Response<Task>.FromError(e.Message);
            }
        }
        /// <summary>
        /// Assigns a task to a user
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>        
        /// <param name="emailAssignee">Email of the user to assign to task to</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AssignTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string emailAssignee)
        {
            try
            {
                log.Info("Trying to assign a task to a user...");
                boardController.GetBoard(creatorEmail, boardName, userEmail).AssignTask(taskId, columnOrdinal, emailAssignee);
                log.Debug("Task has been assigned sucssecfully");
                return new Response();
            }
            catch (Exception e)
            {
                log.Debug("Task has not been assigned sucssecfully ");
                return new Response(e.Message);
            }
        }
    }
}