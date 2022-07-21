using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BuisnessLayer
{
    class Member
    {
        private string email;
        public string Email { get=>email; set { email = value; } }

        private MemberDTO memberDTO;
        public MemberDTO MemberDTO { get=>memberDTO; set { memberDTO = value; } }

        /// <summary>
        /// initialize Member object
        /// </summary>
        /// <param name="email">the email of the member</param>
        /// <param name="boardId">the id of the board whom the user is a member of.</param>
        public Member(string email, string boardId) 
        {
            MemberDTO = new MemberDTO(email, boardId);
            Email = email;
            MemberDTO.Insert();
        }

        /// <summary>
        /// initialize Member object using member data layer object. 
        /// </summary>
        /// <param name="memberDTO">the data object to initialize with.</param>
        public Member(MemberDTO memberDTO)
        {
            Email = memberDTO.Email;
            MemberDTO = memberDTO;
        }

        /// <summary>
        /// deletes the member persisted data.
        /// </summary>
        public void Delete() 
        {
            MemberDTO.Delete();
        }
    }
}
