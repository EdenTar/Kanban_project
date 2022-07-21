 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BuisnessLayer;
using log4net;
using log4net.Config;
using System.Reflection;
using System.IO;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    class BoardService
    {
        private BoardController boardController;

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initialize BoardService object
        /// </summary>
        /// <param name="boardController">The system board controller</param>
        public BoardService(BoardController boardController)
        {
            this.boardController = boardController;
        }


        /// <summary>
        /// Adds a board to the specific user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="name">The name of the new board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AddBoard(string email, string name)
        {
            try
            {
                log.Info("Trying to add a board to specific user...");
                boardController.AddBoard(email, name);
                log.Debug("Board added successfully...");
                return new Response();
            }
            catch (Exception e)
            {
                log.Debug("Failed to add board...");
                return new Response(e.Message);
            }
        }


        /// <summary>
        /// Remove a board from a specific user.
        /// </summary>
        /// <param name="userEmail">Email of the user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the creator of the board.</param>
        /// <param name="name">The name of the board we want to remove</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response RemoveBoard(string userEmail, string creatorEmail, string name)
        {
            try
            {
                log.Info("try to remove board");
                boardController.RemoveBoard(creatorEmail, userEmail, name);
                log.Info("The remove of the board completed succesfully");
                return new Response();
            }
            catch (Exception e)
            {
                log.Info("Remove board failed");
                return new Response(e.Message);
            }
        }


        /// <summary>
        /// return the list of the in progressed tasks
        /// </summary>
        /// <param name="userEmail">Email of the user</param>
        /// <returns> A response<IList<Task>> object. The response should contain a error message in case of an error</returns>
        public Response<IList<Task>> InProgressTasks(string userEmail)
        {
            try
            {
                log.Info("try to get InProgressTasks");
                IList<BuisnessLayer.Task> inProgressTasks = boardController.GetMyInProgress(userEmail);
                //create <IList<Service.Task>> instead of <IList<Buisness.task>>
                IList<Task> InProgress = new List<Task>();
                foreach (BuisnessLayer.Task task in inProgressTasks)
                {
                    InProgress.Add(new Task(task.Id, task.CreationTime, task.Title, task.Description, task.DueDate, userEmail));
                }
                log.Info("InProgressTasks completed succesfully");
                return Response<IList<Task>>.FromValue(InProgress);
            }
            catch (Exception e)
            {
                log.Info("InProgressTasks failed.");
                return Response<IList<Task>>.FromError(e.Message);
            }
        }

        /// <summary>
        /// join user to a board as a new member.
        /// </summary>
        /// <param name="userEmail">the user who join the board</param>
        /// <param name="creatorEmail">the email of the creator of the board</param>
        /// <param name="boardName">the name of the board</param>
        /// <returns>Response object, in case of error with error message.</returns>
        public Response JoinBoard(string userEmail, string creatorEmail, string boardName)
        {
            try
            {
                log.Info("user trying to join a board.");
                boardController.JoinBoard(userEmail, creatorEmail, boardName);
                log.Info("user successfully joined a board.");
                return new Response();
            }
            catch (Exception e)
            {
                log.Info("user failed to join a board");
                return new Response(e.Message);
            }
        }

        /// <summary>
        /// gets all the names of the board the user joined or created.
        /// </summary>
        /// <param name="userEmail">the email of the user</param>
        /// <returns>
        /// Response containing list of string of the board names,
        /// in case of error it contains error message
        /// </returns>
        public Response<IList<String>> GetBoardNames(string userEmail)
        {
            try
            {
                log.Info("user trying to access his board names.");
                List<String> boardNames = boardController.GetBoardNames(userEmail);
                log.Info("user board names were successfully retrived.");
                return Response<IList<String>>.FromValue(boardNames);
            }
            catch (Exception e)
            {
                log.Info("failed to retrive user board names.");
                return Response<IList<String>>.FromError(e.Message);
            }
        }

        /// <summary>
        /// Returns the list of board of a user. The function returns all the board objects the user created or joined.
        /// </summary>
        /// <param name="userEmail">The email of the user. Must be logged-in.</param>
        /// <returns>A response object with a boards object list, instead the response should contain a error message in case of an error</returns>
        public Response<IList<BoardIdentifier>> GetMyBoardsIdentifier(string userEmail)
            {
            try
            {
                log.Info("user trying to access his board.");
                List<BoardIdentifier> boards = boardController.GetMyBoards(userEmail).Select((b)=> new BoardIdentifier(b)).ToList();
                log.Info("user board were successfully retrived.");
                return Response<IList<BoardIdentifier>>.FromValue(boards);
            }
            catch (Exception e)
            {
                log.Info("failed to retrive user board names.");
                return Response<IList<BoardIdentifier>>.FromError(e.Message);
            }
        }

        /// <summary>
        /// Returns the list of the boards that the user didn't created or joined to them.
        /// </summary>
        /// <param name="userEmail">The email of the user. Must be logged-in.</param>
        /// <returns>A response object with a boards object list, instead the response should contain a error message in case of an error</returns>
        public Response<IList<BoardIdentifier>> GetAllBoardsIdentifier(string userEmail)
        {
            try
            {
                log.Info("user trying to access his board.");
                List<BoardIdentifier> boards = boardController.GetAllBoards(userEmail).Select((b) => new BoardIdentifier(b)).ToList();
                log.Info("user board were successfully retrived.");
                return Response<IList<BoardIdentifier>>.FromValue(boards);
            }
            catch (Exception e)
            {
                log.Info("failed to retrive user board names.");
                return Response<IList<BoardIdentifier>>.FromError(e.Message);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="creatorEmail"></param>
        /// <param name="boardName"></param>
        /// <returns></returns>
        public Response<Board> GetBoard(string userEmail, string creatorEmail, string boardName) 
        {
            try
            {
                log.Info("user trying to access specific board");
                Board board = new Board(boardController.GetBoard(creatorEmail, boardName, userEmail));
                log.Info("board was retrieved successfully.");
                return Response<Board>.FromValue(board);
            }
            catch (Exception e) 
            {
                log.Info("failed to retrive board.");
                return Response<Board>.FromError(e.Message);
            }
        }
    }
}
 