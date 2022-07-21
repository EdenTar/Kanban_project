using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using IntroSE.Kanban.DataAccessLayer.DTOs;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.DataAccessLayer
{
    class TaskDalController : DalController
    {
        private const string TaskTableName = "Tasks";
        public const int  IdColumnPosition = 0;
        public const int TaskIdColumnPosition = 1;
        public const int ColumnIdColumnPosition = 2;
        public const int ColumnOrdinalColumnPosition = 3;
        public const int TitleColumnPosition = 4;
        public const int DescriptionColumnPosition = 5;
        public const int CreationTimeColumnPosition = 6;
        public const int DueDateColumnPosition = 7;
        public const int AssigneeColumnPosition = 8;

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public TaskDalController() : base(TaskTableName)
        {

        }

        /// <summary> load all tasks of specific column from db </summary>
        /// <param name="columnId">the id of the column that we want to get his tasks</param>
        /// <returns>return a list of all the tasks (list of TaskDTO)</returns>
        public List<TaskDTO> LoadTasks(string columnId)
        {
            return SelectWhere(TaskDTO.TaskColumnIdColumnName, columnId).Cast<TaskDTO>().ToList();
        }

        /// <summary> convert the data from the db to TaskDTO object </summary>
        /// <param reader>The information of the row from the db</param>
        /// <returns>return a DTO of the current class (that implements DTO)</returns>
        protected override DTO ConvertReaderToObject(SQLiteDataReader reader)
        {

            return new TaskDTO(reader.GetString(IdColumnPosition), reader.GetInt32(TaskIdColumnPosition), reader.GetString(ColumnIdColumnPosition), reader.GetInt32(ColumnOrdinalColumnPosition), reader.GetString(TitleColumnPosition), reader.GetString(DescriptionColumnPosition), Convert.ToDateTime(reader.GetString(CreationTimeColumnPosition)), Convert.ToDateTime(reader.GetString(DueDateColumnPosition)), reader.GetString(AssigneeColumnPosition));
        }

        /// <summary> insert the valued of TaskDTO to the db </summary>
        /// <param task>The task that we will enter his information to the db</param>
        /// <returns>return true if the insert completed successfully</returns>
        public bool Insert(TaskDTO task)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {TaskTableName} ({TaskDTO.TaskIdColumnName} ,{TaskDTO.TaskTaskIdColumnName},{TaskDTO.TaskColumnIdColumnName},{TaskDTO.TaskColumnOrdinalColumnName},{TaskDTO.TaskTitleColumnName},{TaskDTO.TaskDescriptionColumnName},{TaskDTO.TaskCreationTimeColumnName},{TaskDTO.TaskDueDateColumnName},{TaskDTO.TaskAssigneeColumnName}) " +
                        $"VALUES (@idVal,@taskIdVal,@columnIdVal,@columnOrdinalVal,@titleVal,@descriptionVal,@creationTimeVal,@dueDateVal,@assigneeVal);";

                    SQLiteParameter idParam = new SQLiteParameter(@"idVal", task.Id);
                    SQLiteParameter idTaskParam = new SQLiteParameter(@"taskIdVal", task.TaskId);
                    SQLiteParameter idColumnParam = new SQLiteParameter(@"columnIdVal", task.ColumnId);
                    SQLiteParameter columnOrdinalParam = new SQLiteParameter(@"columnOrdinalVal", task.ColumnOrdinal);
                    SQLiteParameter titleParam = new SQLiteParameter(@"titleVal", task.Title);
                    SQLiteParameter descriptionParam = new SQLiteParameter(@"descriptionVal", task.Description);
                    SQLiteParameter creationTimeParam = new SQLiteParameter(@"creationTimeVal", task.CreationTime);
                    SQLiteParameter dueDateParam = new SQLiteParameter(@"dueDateVal", task.DueDate);
                    SQLiteParameter assigneeParam = new SQLiteParameter(@"assigneeVal", task.Assignee);

                    command.Parameters.Add(idParam);
                    command.Parameters.Add(idTaskParam);
                    command.Parameters.Add(idColumnParam);
                    command.Parameters.Add(columnOrdinalParam);
                    command.Parameters.Add(titleParam);
                    command.Parameters.Add(descriptionParam);
                    command.Parameters.Add(creationTimeParam);
                    command.Parameters.Add(dueDateParam);
                    command.Parameters.Add(assigneeParam);
                    command.Prepare();

                    res = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    log.Error("failed to insert task to database");
                    throw new Exception(e.Message);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();

                }
                return res > 0;
            }
        }
    }
}