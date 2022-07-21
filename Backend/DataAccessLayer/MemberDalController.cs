using IntroSE.Kanban.DataAccessLayer;
using IntroSE.Kanban.DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using System.Reflection;
using log4net;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class MemberDalController : DalController
    {
        private const string MembersTableName = "BoardMembers";

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        public MemberDalController() : base(MembersTableName) 
        {
        
        }
        
        /// <summary>
        /// delete from persisted data all of the members of a board.
        /// </summary>
        /// <param name="boardId">the id of the board to delete members from.</param>
        public bool DeleteBoardMembers(string boardId) 
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"DELETE * FROM {MembersTableName} WHERE {MemberDTO.MemberBoardIdColumnName} = @boardIdVal;";
                SQLiteParameter boardIdParam = new SQLiteParameter(@"boardIdVal", boardId);
                command.Parameters.Add(boardIdParam);
                command.Prepare();
                int res = -1;
                try
                {
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    log.Error($"couldn't delete all members of the board({boardId}) from the database");
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

        /// <summary> load all board members from db </summary>
        /// <returns>return a list of all the emails of the board Members </returns>
        public List<MemberDTO> LoadBoardMembers(string BoardId)
        {
            return SelectWhere(MemberDTO.MemberBoardIdColumnName, BoardId).Cast<MemberDTO>().ToList();
        }
        
        /// <summary>
        /// convert sqlite reader to MemberDTO object
        /// </summary>
        /// <param name="reader">the sqlite reader</param>
        /// <returns>the MemberDTO object</returns>
        protected override DTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            return new MemberDTO(reader.GetString(0),reader.GetString(1));
        }

        /// <summary>
        /// insert MemberDTO to the persisted data.
        /// </summary>
        /// <param name="member">the MemberDTO to insert.</param>
        /// <returns>true if insert was successful, false otherwise.</returns>
        public bool Insert(MemberDTO member)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {MembersTableName} ({MemberDTO.MemberBoardIdColumnName},{MemberDTO.MemberEmailColumnName}) " +
                    $"VALUES (@boardIdVal,@memberEmailVal);";

                    SQLiteParameter boardIdParam = new SQLiteParameter(@"boardIdVal", member.BoardId);
                    SQLiteParameter memberEmailParam = new SQLiteParameter(@"memberEmailVal", member.Email);

                    command.Parameters.Add(boardIdParam);
                    command.Parameters.Add(memberEmailParam);
                    command.Prepare();

                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    log.Error($"insert was not successful, problem with db{ex.Message}");
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
                return res > 0;
            }
        }

        /// <summary>
        /// delete MemberDTO from persisted data.
        /// </summary>
        /// <param name="member">the memberDTO to delete.</param>
        /// <returns>true if delete was successful, false otherwise.</returns>
        public bool Delete(MemberDTO member)
        { 
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"DELETE FROM {MembersTableName} WHERE {MemberDTO.MemberBoardIdColumnName}=@boardIdVal AND {MemberDTO.MemberEmailColumnName}=@memberEmailVal ;";  

                    SQLiteParameter boardIdParam = new SQLiteParameter(@"boardIdVal", member.BoardId);
                    SQLiteParameter memberEmailParam = new SQLiteParameter(@"memberEmailVal", member.Email);

                    command.Parameters.Add(boardIdParam);
                    command.Parameters.Add(memberEmailParam);
                    command.Prepare();

                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    log.Error($"failed to remove member from database");
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
