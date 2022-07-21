using IntroSE.Kanban.DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    class BoardDTO : DTO
    {
        // Boards table column names 
        public const string BoardIDColumnName = "id";
        public const string BoardNameColumnName = "boardName";
        public const string BoardEmailOfCreatorColumnName = "emailOfCreator";
        public const string BoardTotalNumOfTasksColumnName = "totalNumOfTasks";

        private string boardName;
        public string BoardName
        {
            get => boardName;
            set
            {
                boardName = value;
                if (Persisted) 
                {
                    this._controller.Update(BoardIDColumnName, Id, BoardNameColumnName, boardName);
                }
            }
        }

        private string emailOfCreator;
        public string EmailOfCreator
        {
            get => emailOfCreator;
            set
            {
                emailOfCreator = value;
                if (Persisted)
                {
                    this._controller.Update(BoardIDColumnName, Id, BoardEmailOfCreatorColumnName, emailOfCreator);
                }
            }
        }
        private int totalNumOfTasks;
        public int TotalNumOfTasks
        {
            get => totalNumOfTasks;
            set
            {
                totalNumOfTasks = value;
                if (Persisted) 
                {
                    this._controller.Update(BoardIDColumnName, Id, BoardTotalNumOfTasksColumnName, totalNumOfTasks);
                }
            }
        }
        private bool persisted;
        public bool Persisted 
        {
            get => persisted;
            set
            {
                persisted = value;
            }
        }

        /// <summary>
        /// initialize BoardDTO object.
        /// </summary>
        /// <param name="boardId">the id of the board.</param>
        /// <param name="emailOfCreator">the email of the creator of the board.</param>
        /// <param name="boardName">the name of the board</param>
        /// <param name="totalNumOfTasks">the number of tasks in the board</param>
        public BoardDTO(string boardId, string emailOfCreator, string boardName, int totalNumOfTasks) : base(new BoardDalController())
        {
            this.Persisted = false;
            this.Id = boardId;
            this.BoardName = boardName;
            this.EmailOfCreator = emailOfCreator;
            this.TotalNumOfTasks = totalNumOfTasks;
        }

        /// <summary>
        /// insert board to persisted data.
        /// </summary>
        public void Insert() 
        {
           ((BoardDalController)this._controller).Insert(this);
            Persisted = true;
        }

        /// <summary>
        /// delete board from persisted data.
        /// </summary>
        public void Delete() 
        {
            ((BoardDalController)_controller).Delete(BoardIDColumnName, this);
        }
    }
}
