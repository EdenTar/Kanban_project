using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Frontend.Model
{
    public abstract class BackendController
    {
        private Service service;
        public Service Service { get => service; set { service = value; } }
        public BackendController(Service service)
        {
            this.Service = service;
        }

        public BackendController()
        {
            //Directory.SetCurrentDirectory(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "kanban.db")));
            this.Service = new Service();
        }
    }
}


