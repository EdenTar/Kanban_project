using IntroSE.Kanban.DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using log4net;
using System.Reflection;
using IntroSE.Kanban.Backend.BuisnessLayer;

namespace IntroSE.Kanban.DataAccessLayer
{
    internal abstract class DalController
    {
        protected readonly string _connectionString;
        private readonly string _tableName;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// initialize DalController object.
        /// </summary>
        /// <param name="tableName">the name of the table to connect to and execute on.</param>
        public DalController(string tableName)
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "kanban.db"));
            this._connectionString = $"Data Source={path}; Version=3;";
            this._tableName = tableName;
        }

        /// <summary>
        /// update persisted data, update one column to new value.
        /// </summary>
        /// <param name="idColumnName">the name of the column of the id</param>
        /// <param name="id">the id of the row to update</param>
        /// <param name="attributeName">the name of the column to update</param>
        /// <param name="attributeValue">the new value to enter</param>
        /// <returns>true if updated in persisted data, false otherwise.</returns>
        public bool Update(string idColumnName, string id, string attributeName, object attributeValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"UPDATE {_tableName} SET [{attributeName}]=@attributeValueVal WHERE {idColumnName}=@idVal;";
                SQLiteParameter idParam = new SQLiteParameter(@"idVal", id);
                SQLiteParameter attributeValueParam = new SQLiteParameter(@"attributeValueVal", attributeValue);
                command.Parameters.Add(idParam);
                command.Parameters.Add(attributeValueParam);
                command.Prepare();
                try
                {
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    log.Error("failed to update");
                    throw new Exception(e.Message);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();

                }

            }
            return res > 0;
        }

        protected List<DTO> Select()
        {
            List<DTO> results = new List<DTO>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"SELECT * FROM {_tableName};";
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        results.Add(ConvertReaderToObject(dataReader));

                    }
                }
                catch(Exception e)
                {
                    log.Error("failed in load data from the the db");
                    throw new Exception(e.Message);
                }
                finally
                {
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }

                    command.Dispose();
                    connection.Close();
                }

            }
            return results;
        }

        protected List<DTO> SelectWhere(string columnName, string compareValue)
        {
            List<DTO> results = new List<DTO>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"SELECT * FROM {_tableName} WHERE {columnName} = @compareValue;";
                SQLiteParameter compareValueParam = new SQLiteParameter(@"compareValue", compareValue);
                command.Parameters.Add(compareValueParam);
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        results.Add(ConvertReaderToObject(dataReader));
                    }
                }
                catch(Exception e)
                {
                    log.Error("failed in load data from the the db");
                    throw new Exception(e.Message);
                }
                finally
                {
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }

                    command.Dispose();
                    connection.Close();
                }

            }
            return results;
        }

        protected abstract DTO ConvertReaderToObject(SQLiteDataReader reader);

        public bool Delete(string idColumn ,DTO DTOObj)
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"DELETE FROM {_tableName} WHERE {idColumn} = @idValue;";
                SQLiteParameter idParam = new SQLiteParameter(@"idValue", DTOObj.Id);
                command.Parameters.Add(idParam);
                try
                {
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
            return res > 0;
        }

        public bool DeleteAllData()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"delete from {_tableName}"
                };
                int res = -1;
                try
                {
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch(Exception e)
                {
                    log.Error("failed in delete data from the the db");
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
