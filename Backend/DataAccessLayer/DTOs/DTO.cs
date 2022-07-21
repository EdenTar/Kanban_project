using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.DataAccessLayer.DTOs
{
    internal abstract class DTO
    {
       
        protected DalController _controller;
        public virtual string Id { get; set; } 
        protected DTO(DalController controller)
        {
            _controller = controller;
        }

    }
}