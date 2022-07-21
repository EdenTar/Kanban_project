using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = IntroSE.Kanban.Backend.ServiceLayer.Task;

namespace IntroSE.Kanban.Frontend.Model
{
    public class ColumnController: BackendController
    {
        public ColumnController(Service service):base(service)
        {

        }

        /// <summary>
        /// Reaches to the buisness layer in order rename the column name
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="creatorEmail"></param>
        /// <param name="boardName"></param>
        /// <param name="columnOrdinal"></param>
        /// <param name="newColumnName"></param>
        public void RenameColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, string newColumnName)
        {
            Response column = Service.RenameColumn(userEmail, creatorEmail, boardName, columnOrdinal, newColumnName);
            if (column.ErrorOccured)
            {
                throw new Exception(column.ErrorMessage);
            }
        }

        /// <summary>
        /// Reaches to the buisness layer in order to add a new cloumn
        /// </summary>
        /// <param name="userM"></param>
        /// <param name="boardM"></param>
        /// <param name="columnOrdinal"></param>
        /// <param name="columnName"></param>
        /// <returns>New Column modle object</returns>
        public ColumnM AddColumn(UserM userM, BoardM boardM, int columnOrdinal, string columnName)
        {
            Response column = Service.AddColumn(userM.Email, boardM.CreatorEmail, boardM.BoardName, columnOrdinal, columnName);
            if (column.ErrorOccured)
            {
                throw new Exception(column.ErrorMessage);
            }
            ObservableCollection<TaskM> tasks = new ObservableCollection<TaskM>();
            ColumnM columnM = new ColumnM(this, columnName, -1, columnOrdinal,boardM,userM);
            columnM.Tasks = tasks;
            if (boardM.Columns.Count == columnOrdinal)
            {
                boardM.Columns.Add(columnM);
            }
            else
            {
                boardM.Columns.Insert(columnOrdinal, columnM);
            }
            return columnM;
        }

        /// <summary>
        /// Reaches to the buisness layer in order to remove a cloumn
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="creatorEmail"></param>
        /// <param name="boardName"></param>
        /// <param name="columnOrdinal"></param>
        public void RemoveColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {

            Response column = Service.RemoveColumn(userEmail, creatorEmail, boardName, columnOrdinal);
            if (column.ErrorOccured)
            {
                throw new Exception(column.ErrorMessage);
            }

        }

        /// <summary>
        ///  Reaches to the buisness layer in order to limit a column
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="creatorEmail"></param>
        /// <param name="boardName"></param>
        /// <param name="columnOrdinal"></param>
        /// <param name="limit"></param>
        public void LimitColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int limit)
        {
            Response column = Service.LimitColumn(userEmail, creatorEmail, boardName, columnOrdinal,limit);
            if (column.ErrorOccured)
            {
                throw new Exception(column.ErrorMessage);
            }
        }

        /// <summary>
        ///  Reaches to the buisness layer in order to move a column
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="creatorEmail"></param>
        /// <param name="boardName"></param>
        /// <param name="columnOrdinal"></param>
        /// <param name="shiftSize"></param>
        public void MoveColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int shiftSize)
        {
            Response column = Service.MoveColumn(userEmail, creatorEmail, boardName, columnOrdinal, shiftSize);
            if (column.ErrorOccured)
            {
                throw new Exception(column.ErrorMessage);
            }
        }

        public ObservableCollection<TaskM> GetColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, ColumnM columnM) 
        {
            Response<IList<Task>> Tasks = Service.GetColumn(userEmail, creatorEmail, boardName, columnOrdinal);
            if (Tasks.ErrorOccured)
            {
                throw new Exception(Tasks.ErrorMessage);
            }
            return new ObservableCollection<TaskM>(Tasks.Value.Select((t)=>new TaskM(new TaskController(Service), t.Id, t.Title, t.Description, t.emailAssignee, t.DueDate, t.CreationTime)));
        }

        public void UpdateColumnName(string userEmail, string creatorEmail, string boardName, int columnOrdinal, string name) 
        {
            Response response = Service.RenameColumn(userEmail, creatorEmail, boardName, columnOrdinal, name);
            if (response.ErrorOccured)
                throw new Exception(response.ErrorMessage);
        }
        public void UpdateColumnLimit(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int limit)
        {
            Response response = Service.LimitColumn(userEmail, creatorEmail, boardName, columnOrdinal, limit);
            if (response.ErrorOccured)
                throw new Exception(response.ErrorMessage);
        }
    }
}



