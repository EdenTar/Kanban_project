using IntroSE.Kanban.DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    class ColumnDTO : DTO, IComparable
    {
        public const string ColumnIdColumnName = "id";
        public const string ColumnBoardIdColumnName = "boardId";
        public const string ColumnColumnOrdinalColumnName = "columnOrdinal";
        public const string ColumnLimitColumnName = "columnLimit";
        public const string ColumnNameColumnName = "name";

        private string name;    
        public string Name 
        {
            get => name;
            set
            {
                name = value;
                if (Persisted) {
                    _controller.Update(ColumnIdColumnName, id, ColumnNameColumnName, Name);
                }
            }
        }
        private int limit;
        public int Limit
        {
            get => limit;
            set 
            {
                limit = value;
                if (Persisted) {
                    _controller.Update(ColumnIdColumnName, Id,ColumnLimitColumnName,  Limit);
                }
            }
        }
        private string id;
        public override string Id
        {
            get => id;
            set {
                if (Persisted) {
                    _controller.Update(ColumnIdColumnName, id, ColumnIdColumnName, value);
                }
                id = value;
            }
        }

        private string boardId;
        public string BoardId 
        {
            get => boardId;
            set {
                boardId = value;
                if (Persisted) {
                    _controller.Update(ColumnIdColumnName, Id,ColumnBoardIdColumnName, BoardId);
                }
            }
        }
        private int columnOrdinal;
        public int ColumnOrdinal 
        {
            get => columnOrdinal;
            set
            {
                columnOrdinal = value;
                if (Persisted) {
                    _controller.Update(ColumnIdColumnName,Id, ColumnColumnOrdinalColumnName, ColumnOrdinal);
                }
            }
        }
        private bool persisted;
        public bool Persisted{ get => persisted; set { persisted = value; } }

        /// <summary>
        /// initialize ColumnDTO object.
        /// </summary>
        /// <param name="columnId">the id of the column.</param>
        /// <param name="columnOrdinal">the ordinal location of the column.</param>
        /// <param name="limit">the limit of the column.</param>
        /// <param name="boardId">the id of the board that the column is in.</param>
        /// <param name="name">the name of the columnn.</param>
        public ColumnDTO(string columnId, int columnOrdinal, int limit, string boardId, string name) : base(new ColumnDalController())
        {
            Persisted = false;
            Id = columnId;
            BoardId = boardId;
            ColumnOrdinal = columnOrdinal;
            Limit = limit;
            Name = name;
        }

        /// <summary>
        /// insert the column to persisted data.
        /// </summary>
        public void InsertColumn() {
            ((ColumnDalController)_controller).Insert(this);
            Persisted = true;
        }

        /// <summary>
        /// delete the column from persisted data.
        /// </summary>
        public void Delete() 
        {
            ((ColumnDalController)_controller).Delete(ColumnIdColumnName,this);
        }

        public int CompareTo(object obj)
        {
            if (obj.GetType() != this.GetType()) 
            {
                throw new Exception("cant compare column dto to non columnDTO");
            }
            if (((ColumnDTO)obj).ColumnOrdinal > ColumnOrdinal)
            {
                return -1;
            }
            else 
            {
                return 1;
            }
        }
    }
}
