using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Frontend.Model
{
    public class DataController : BackendController
    {
        public DataController(Service service) : base(service) { }
        public DataController() : base() { }
        public void LoadData() 
        {
            Response response = Service.LoadData();
            if (response.ErrorOccured) 
            {
                throw new Exception(response.ErrorMessage);
            }
        }

        public void DeleteData() 
        {
            Response response = Service.DeleteData();
            if (response.ErrorOccured) 
            {
                throw new Exception(response.ErrorMessage);
            }
        }
    }
}
