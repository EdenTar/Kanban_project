using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using IntroSE.Kanban.DataAccessLayer;
using IntroSE.Kanban.DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using log4net;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class BoardDalController : DalController
    {
        private const string BoardTableName = "Boards";
        public int IdColumnPosition = 0;
        public int EmailOfCreatorColumnPosition = 1;
        public int BoardNameColumnPosition = 2;
        public int TotalNumOfTasksColumnPosition = 3;

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public BoardDalController() : base(BoardTableName)
        {

        }

        /// <summary> load all boards from db </summary>
        /// <returns>return a list of all the board (list of BoardDTO)</returns>
        public List<BoardDTO> LoadAllBoards()
        {
            return Select().Cast<BoardDTO>().ToList();
        }

        /// <summary> convert the data from the db to BoardDTO object </summary>
        /// <param reader>The information of the row from the db</param>
        /// <returns>return a DTO of the current class (that implements DTO)</returns>
        protected override DTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            return new BoardDTO(reader.GetString(IdColumnPosition), reader.GetString(EmailOfCreatorColumnPosition), reader.GetString(BoardNameColumnPosition), reader.GetInt32(TotalNumOfTasksColumnPosition));
        }

        /// <summary>
        /// inserts BoardDTO to persisted data.
        /// </summary>
        /// <param name="board">the board to insert.</param>
        /// <returns>return true if insertion was successful, false otherwise</returns>
        public bool Insert(BoardDTO board)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {BoardTableName} ({BoardDTO.BoardIDColumnName} ,{BoardDTO.BoardEmailOfCreatorColumnName} ,{BoardDTO.BoardNameColumnName},{BoardDTO.BoardTotalNumOfTasksColumnName}) " +
                    $"VALUES (@idVal,@emailOfCreatorVal,@boardNameVal,@totalNumOfTasksVal);";

                    SQLiteParameter idParam = new SQLiteParameter(@"idVal", board.Id);
                    SQLiteParameter boardNameParam = new SQLiteParameter(@"boardNameVal", board.BoardName);
                    SQLiteParameter emailOfCreatorParam = new SQLiteParameter(@"emailOfCreatorVal", board.EmailOfCreator);
                    SQLiteParameter totalNumOfTasksParam = new SQLiteParameter(@"totalNumOfTasksVal", board.TotalNumOfTasks);
                    
                    command.Parameters.Add(idParam);
                    command.Parameters.Add(boardNameParam);
                    command.Parameters.Add(emailOfCreatorParam);
                    command.Parameters.Add(totalNumOfTasksParam);
                    command.Prepare();

                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    log.Error($"board insert failed");
                    throw new Exception(ex.Message);
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
