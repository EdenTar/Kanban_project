using IntroSE.Kanban.DataAccessLayer.DTOs;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    internal class UserDTO : DTO
    {

        public const string emailColumnName = "email";
        public const string passwordColumnName = "password";

        private string email;
        public string Email { get => email; set { email = value; } }

        private string password;
        public string Password{ get => password; set { password = value; } }

        private bool presisted = false;
        public bool Presisted { get => presisted; set { presisted = value; } }

        /// <summary>
        /// initialize UserDTO object.
        /// </summary>
        /// <param name="email">the email of the user</param>
        /// <param name="password">the password of the user</param>
        public UserDTO(string email, string password) : base(new UserDalController())
        {
            Presisted = false;
            Email = email;
            Password = password;
        }

        ///<summary>This method Insert the User email and password to the db.</summary>
        public void Insert()
        {
            ((UserDalController)_controller).Insert(this);
            Presisted = true;
        }

    }
}