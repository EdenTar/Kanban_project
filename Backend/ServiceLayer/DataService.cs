using IntroSE.Kanban.Backend.BuisnessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Config;
using System.Reflection;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    class DataService
    {
        private UserController userController;
        private BoardController boardController;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public DataService(UserController userController, BoardController boardController)
        {
            this.userController = userController;
            this.boardController = boardController;
        }

        /// <summary>
        ///  load all data from all the tabled in the db.
        /// </summary>
        /// <returns>
        ///  Response object, which contains error message in case of error,
        ///  otherwise the load data completed succesfully.
        /// </returns>
        public Response LoadData()
        {
            try
            {
                log.Info($"trying to load all data");
                userController.LoadUsers();
                log.Debug($"The load of the users completed succesfully");
                boardController.LoadBoards();
                log.Debug($"The load of the boards completed succesfully");
                return new Response();
            }
            catch (Exception e)
            {
                log.Debug("load data failed");
                return new Response(e.Message);
            }
        }

        /// <summary>
        ///  delete all data from each dictionary and list.
        /// </summary>
        /// <returns>
        ///  Response object, which contains error message in case of error,
        ///  otherwise the delete data completed succesfully.
        /// </returns>
        public Response DeleteData()
        {
            try
            {
                log.Info($"trying to delete all data");
                userController.DeleteUsers();
                log.Debug($"The delete of the users completed succesfully");
                boardController.DeleteBoardsData();
                log.Debug($"The delete of the boards completed succesfully");
                return new Response();
            }
            catch (Exception e)
            {
                log.Debug("delete data failed");
                return new Response(e.Message);
            }
        }
    }
}
