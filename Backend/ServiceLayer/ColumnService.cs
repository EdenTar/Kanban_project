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

    class ColumnService
    {
        private BoardController boardController;

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        /// <summary>
        /// Initialize ColumnService object.
        /// </summary>
        /// <param name="boardController">The system's boardController</param>
        public ColumnService(BoardController boardController) 
        {
            this.boardController = boardController;
        }


        /// <summary>
        /// Gets the name of a specific column.
        /// </summary>
        /// <param name="userEmail">The email of the user requesting the column name</param>
        /// <param name="creatorEmail">The email of the creator of the board the column is in</param>
        /// <param name="name">The name of the board the column is in</param>
        /// <param name="columnOrdinal">The ordinal number of the column</param>
        /// <returns>A response object with value set to string of the column name. The response should contain a error message in case of an error</returns>
        public Response<string> GetColumnName(string userEmail, string creatorEmail, string name, int columnOrdinal) 
        {
            try
            {
                log.Info("Trying to get the name of a column...");
                string columnName = this.boardController.GetBoard(creatorEmail, name, userEmail).GetColumn(userEmail, columnOrdinal).Name;
                log.Debug("Column name was returned successfully...");
                return Response<string>.FromValue(columnName); 
            }
            catch(Exception e)
            {

                log.Debug("Failed to return column name...");
                return Response<string>.FromError(e.Message); 
            }
        }


        /// <summary>
        /// Limit the amount of tasks in a specific column.
        /// </summary>
        /// <param name="userEmail">The email of the user requesting to limit8 the column.</param>
        /// <param name="creatorEmail">The email of the creator of the board of the desired column</param>
        /// <param name="boardName">The name of the board of the desired column</param>
        /// <param name="columnOrdinal">The ordinal number of the desired column</param>
        /// <param name="limit">The wanted maximum number of tasks in the column</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response LimitColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int limit)
        {
            try
            {
                log.Info("Trying to limit the column...");
                this.boardController.GetBoard(creatorEmail, boardName, userEmail).GetColumn(userEmail, columnOrdinal).MaxNumOfTasks = limit;
                log.Debug("The column was Limited successfully...");
                return new Response();
            }
            catch (Exception e)
            {
                log.Debug("The column couldn't be limited...");
                return new Response(e.Message);
            }
        }


        /// <summary>
        /// return column (by email, boardname and columnOrdinal)
        /// </summary>
        /// <param name="userEmail">Email of the user requesting the column.</param>
        /// <param name="creatorEmail">Email of the creator of the board</param>
        /// <param name="boardName">the name of the board that contains the column</param>
        /// <param name="columnOrdinal">the identify number of the column</param>
        /// <returns> A response<IList<Task>> object. The response should contain a error message in case of an error</returns>
        public Response<Column> GetColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            try
            {
                log.Info("Try to get column...");
                Column column = new Column(this.boardController.GetBoard(creatorEmail, boardName, userEmail).GetColumn(userEmail, columnOrdinal));
                //create <IList<Service.Task>> instead of <IList<Buisness.task>>
                log.Debug("GetColumn completed succesfully");
                return Response<Column>.FromValue(column);
            }
            catch (Exception e)
            {
                log.Debug("GetColumn failed");
                return Response<Column>.FromError(e.Message);
            }
        }


        /// <summary>
        /// Limit the amount of tasks in specific column.
        /// </summary>
        /// <param name="userEmail">The email of the requesting the limit, must be logged in and a member.</param>
        /// <param name="creatorEmail">The email of the creator of the board the column is in</param>
        /// <param name="boardName">The name of the board the column is in</param>
        /// <param name="columnOrdinal">The ordinal number of the column</param>
        /// <returns>A response object with the value set to the column limit. The response should contain a error message in case of an error</returns>
        public Response<int> GetColumnLimit(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            try
            {
                log.Info("User is trying to get the column limit...");
                int columnLimit = this.boardController.GetBoard(creatorEmail, boardName, userEmail).GetColumn(userEmail, columnOrdinal).MaxNumOfTasks;
                log.Debug("Returned the column limit successfully..");
                return Response<int>.FromValue(columnLimit);
            }
            catch (Exception e) 
            {
                log.Debug("The column limit return failed...");
                return Response<int>.FromError(e.Message);
            }
        }

        /// <summary>
        /// Adds column to the board. user must be logged in and a member of the board.
        /// </summary>
        /// <param name="userEmail">the email of the user trying to add column, must be logged in and member of board.</param>
        /// <param name="creatorEmail">the email of the creator of the board.</param>
        /// <param name="boardName">the name of the board to add a column to.</param>
        /// <param name="columnOrdinal">the location to add the column to.</param>
        /// <param name="columnName">the name of the new column.</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AddColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal,string columnName)
        {
            try
            {
                log.Info("user is trying to add column...");
                boardController.GetBoard(creatorEmail, boardName, userEmail).AddColumn(userEmail, columnOrdinal ,columnName);
                log.Debug("the column was added successfully...");
                return new Response();
            }
            catch (Exception e)
            {
                log.Debug("failed to add the column...");
                return new Response(e.Message);
            }
        }
        
        /// <summary>
        /// Removes a column. user must be logged in and a member of the board.
        /// </summary>
        /// <param name="userEmail">the user trying to remove the board, must be logged in and member of the board.</param>
        /// <param name="creatorEmail">the email of the creator of the board.</param>
        /// <param name="boardName">the name of the board to remove the column from.</param>
        /// <param name="columnOrdinal">the ordinal location of the column.</param>
        /// <returns>Response object, if failed will contain error message.</returns>
        public Response RemoveColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal) 
        {
            try
            {
                log.Info("user is trying to remove column...");
                boardController.GetBoard(creatorEmail, boardName, userEmail).RemoveColumn(userEmail, columnOrdinal);
                log.Debug("the column was removed successfully...");
                return new Response();
            }
            catch (Exception e) 
            {
                log.Debug("the removal of the column wasn't successful...");
                return new Response(e.Message);
            }
        }

        /// <summary>
        /// move column shiftSize places right, if negetive then to the left.
        /// </summary>
        /// <param name="userEmail">the user trying to move the column, must be logged in and a member of the board.</param>
        /// <param name="creatorEmail">the email of the creator of the board.</param>
        /// <param name="boardName">the name of the board.</param>
        /// <param name="columnOrdinal">the ordinal location of the column.</param>
        /// <param name="shiftSize">the amount to shift to the right, if negetive shift to the left.</param>
        /// <returns>Response object, in case of error contains error message.</returns>
        public Response MoveColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int shiftSize) 
        {
            try
            {
                log.Info("user is trying to move a column...");
                boardController.GetBoard(creatorEmail, boardName, userEmail).MoveColumn(userEmail, columnOrdinal, shiftSize);
                log.Debug("column was moved successfully...");
                return new Response();
            }
            catch (Exception e) 
            {
                log.Debug("column failed to be moved...");
                return new Response(e.Message);
            }
        }

        /// <summary>
        /// rename a column.
        /// </summary>
        /// <param name="userEmail">the email of the user, must be logged in and a member of the board.</param>
        /// <param name="creatorEmail">the email of the creator of the board.</param>
        /// <param name="boardName">the name of the board.</param>
        /// <param name="columnOrdinal">the ordinal location of the column.</param>
        /// <param name="newColumnName">the new name of the column.</param>
        /// <returns>response object, if failed it contains error message.</returns>
        public Response RenameColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, string newColumnName) 
        {
            try
            {
                log.Info("user trying to rename column...");
                boardController.GetBoard(creatorEmail, boardName, userEmail).GetColumn(userEmail, columnOrdinal).Name = newColumnName;
                log.Debug("successfully renamed column...");
                return new Response();
            }
            catch (Exception e) 
            {
                log.Debug("failed to rename column...");
                return new Response(e.Message);
            }
        }
    }
}
