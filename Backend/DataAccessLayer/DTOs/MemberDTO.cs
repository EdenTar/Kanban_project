using IntroSE.Kanban.DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    class MemberDTO : DTO
    {
        public const string MemberEmailColumnName  = "emailOfMember";
        public const string MemberBoardIdColumnName = "boardId";

        private string email;
        public string Email { get => email; set {email = value; } }

        private string boardId;
        public string BoardId { get => boardId; set {boardId = value; } }

        /// <summary>
        /// initialize MemberDTO obejct.
        /// </summary>
        /// <param name="email">the email of the member.</param>
        /// <param name="boardId">the board the member is a member of.</param>
        public MemberDTO(string email, string boardId) : base(new MemberDalController()) 
        {
            Email = email;
            BoardId = boardId;
        }

        /// <summary>
        /// insert member to persisted data.
        /// </summary>
        public void Insert() {
            ((MemberDalController)_controller).Insert(this);
        }

        /// <summary>
        /// delete member from persisted data.
        /// </summary>
        public void Delete() 
        {
            ((MemberDalController)_controller).Delete(this);
        }
    }
}
