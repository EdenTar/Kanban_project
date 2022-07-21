
﻿using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using IntroSE.Kanban.DataAccessLayer;
using IntroSE.Kanban.DataAccessLayer.DTOs;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Threading.Tasks;


namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class ColumnDalController : DalController
    {
        // name of table
        private const string ColumnTableName = "Columns";
        
        // position of the attributes in the table
        public const int IdColumnPosition = 0;
        public const int ColumnOrdinalColumnPosition = 1;
        public const int ColumnLimitColumnPosition= 2;
        public const int BoardIdColumnPosition = 3;
        public const int NameColumnPosition = 4;

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ColumnDalController() : base(ColumnTableName) { }

        /// <summary> load all columns of specific board from db </summary>
        /// /// <param BoardId="BoardId">the id of the board that we want to get his columns</param>
        /// <returns>return a list of all the columns (list of ColumnDTO)</returns>
        public List<ColumnDTO> LoadAllColumns(string BoardId)
        {
            return SelectWhere(ColumnDTO.ColumnBoardIdColumnName, BoardId).Cast<ColumnDTO>().ToList();
        }

        /// <summary> convert the data from the db to ColumnDTO object </summary>
        /// <param reader>The information of the row from the db</param>
        /// <returns>return a DTO of the current class (that implements DTO)</returns>
        protected override DTO ConvertReaderToObject(SQLiteDataReader reader)
        {
           return new ColumnDTO(reader.GetString(IdColumnPosition), reader.GetInt32(ColumnOrdinalColumnPosition), reader.GetInt32(ColumnLimitColumnPosition), reader.GetString(BoardIdColumnPosition),reader.GetString(NameColumnPosition));
        }

        /// <summary>
        /// insert ColumnDTO to persisted data.
        /// </summary>
        /// <param name="column">ColumnDTO to persist</param>
        /// <returns>true if insertion was successful, false otherwise.</returns>
        public bool Insert(ColumnDTO column)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {ColumnTableName} ({ColumnDTO.ColumnIdColumnName} ,{ColumnDTO.ColumnBoardIdColumnName},{ColumnDTO.ColumnColumnOrdinalColumnName},{ColumnDTO.ColumnLimitColumnName},{ColumnDTO.ColumnNameColumnName}) " +
                    $"VALUES (@idVal,@boardIdVal,@columnOrdinalVal,@limitVal,@nameVal);";

                    SQLiteParameter idParam = new SQLiteParameter(@"idVal", column.Id);
                    SQLiteParameter boardIdParam = new SQLiteParameter(@"boardIdVal", column.BoardId);
                    SQLiteParameter columnOrdinalParam = new SQLiteParameter(@"columnOrdinalVal", column.ColumnOrdinal);
                    SQLiteParameter limitParam = new SQLiteParameter(@"limitVal", column.Limit);
                    SQLiteParameter nameParam = new SQLiteParameter(@"nameVal", column.Name);

                    command.Parameters.Add(idParam);
                    command.Parameters.Add(boardIdParam);
                    command.Parameters.Add(columnOrdinalParam);
                    command.Parameters.Add(limitParam);
                    command.Parameters.Add(nameParam);
                    command.Prepare();

                    res = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    log.Error("insert Column to database failed");
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
