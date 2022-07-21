using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using IntroSE.Kanban.DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using IntroSE.Kanban.DataAccessLayer;
using log4net;
using IntroSE.Kanban.Backend.BuisnessLayer;
using System.Reflection;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class UserDalController : DalController
    {
        private const string UsersTableName = "Users";

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public UserDalController() : base(UsersTableName)
        {

        }

        ///<summary>This method Insert the User email and password to the db.</summary>
        ///<param name="userDTO">the information of the user that we need to insert to the db.</param>
        public bool Insert(UserDTO userDTO)
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "kanban.db"));
            string connectionString = $"Data Source={path}; Version=3;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    command.CommandText = $"INSERT INTO {UsersTableName} ({UserDTO.emailColumnName} , {UserDTO.passwordColumnName})" +
                        $"VALUES (@emailVal, @passwordVal);";
                    SQLiteParameter emailVal = new SQLiteParameter(@"emailVal", userDTO.Email);
                    SQLiteParameter passwordVal = new SQLiteParameter(@"passwordVal", userDTO.Password);

                    command.Parameters.Add(emailVal);
                    command.Parameters.Add(passwordVal);
                    connection.Open();
                    command.Prepare();
                    res = command.ExecuteNonQuery();
                }
                catch(Exception e)
                {
                    log.Error("failed in insert user to the db");
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

        /// <summary> convert the data from the db to UserDTO object </summary>
        /// <param reader>The information of the row from the db</param>
        /// <returns>return a DTO of the current class (that implements DTO)</returns>
        protected override DTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            UserDTO result = new UserDTO(reader.GetString(0), reader.GetString(1));
            return result;
        }

        ///<summary>This method return list of all the users in the db.</summary>       
        ///<returns>list of all the users in the db</returns>
        internal List<UserDTO> LoadAllUsers()
        {
            List<UserDTO> result = Select().Cast<UserDTO>().ToList();
            return result;
        }

    }
}