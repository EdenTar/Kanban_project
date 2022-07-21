using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class BoardIdentifier
    {
        private string name;
        public string Name { get => name; set { name = value; } }
        private string creatorEmail;
        public string CreatorEmail { get => creatorEmail; set { creatorEmail = value; } }
        internal BoardIdentifier(BuisnessLayer.Board board)
        {
            Name = board.Name;
            CreatorEmail = board.EmailOfCreator;
            
        }
    }
}
