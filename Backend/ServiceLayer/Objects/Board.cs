using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntroSE.Kanban.Backend.BuisnessLayer;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class Board
    {
        private string name;
        public string Name { get => name; set { name = value; } }
        private string creatorEmail;
        public string CreatorEmail { get => creatorEmail; set { creatorEmail = value; } }
        private List<Column> columns;
        public List<Column> Columns { get => columns; set { columns = value; } }

        /// <summary>
        /// sets the columns with a list of columns from the buisness layer
        /// </summary>
        /// <param name="columns">list of columns from buisness layer</param>
        private void SetColumns(List<BuisnessLayer.IColumn> columns) 
        {   
            Columns = new List<Column>();
            foreach (BuisnessLayer.Column column in columns)
            {
                Columns.Add(new Column(column));
            }
        }
        /// <summary>
        /// initialize new Board object using Board object from buisness Layer
        /// </summary>
        /// <param name="board">Board object from buissness Layer</param>
        internal Board(BuisnessLayer.Board board) 
        {
            Name = board.Name;
            CreatorEmail = board.EmailOfCreator;
            SetColumns(board.Columns);
        }
    }
}
