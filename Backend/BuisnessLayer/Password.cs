using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using System.Reflection;
using System.IO;

namespace IntroSE.Kanban.Backend.BuisnessLayer
{
    class Password
    {
        private string password;
        private readonly int maxLengthOfPassword = 20;
        private readonly int minLengthOfPassword = 4;
        private readonly List<string> commonPasswords = new List<string>() {"123456", "123456789", "qwerty", "password", "1111111", "12345678", "abc123", "1234567", "password1", "12345", "1234567890", "123123", "000000", "Iloveyou", "1234", "1q2w3e4r5t", "Qwertyuiop", "123", "Monkey", "Dragon" };
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public Password(string pass)
        {
            //verify that the password answer the requirements
            if (!ValidatePasswordRules(pass))
            {
                log.Error("the password structure is incorrect");
                throw new Exception($"Password structure incorrect");
            }
            this.password = pass;
        }


        ///<summary>This method compares the current password to the password we received.</summary>
        /// <param name="newPass">The password we want to verify</param>
        /// <returns>true if the current password equal to newPass, false if not.</returns>
        public bool ValidatePasswordMatch(string comparePassword)
        {
            return this.password.Equals(comparePassword);
        }


        ///<summary>This method check if the password is valid (according to the password rules).</summary>
        /// <param name="pass">The password we want to check by the password rules</param>
        /// <returns>true if the password is valid.</returns>
        private bool ValidatePasswordRules(string pass)
        {
            //check password length
            if (pass.Length > maxLengthOfPassword || pass.Length < minLengthOfPassword)
            {
                log.Warn("the length of the password is too short/to long");
                return false;
            }

            bool numbers = false, uppercaseLetter = false, smallCharacter = false;
            //verify that the password include at least one uppercase letter, one small character and a number.
            foreach (char c in pass)
            {
                if (c >= 'a' & c <= 'z')
                {
                    smallCharacter = true;
                }
                else if (c >= 'A' & c <= 'Z')
                {
                    uppercaseLetter = true;
                }
                else if (c >= '0' & c <= '9')
                {
                    numbers = true;
                }
            }
            if (smallCharacter & uppercaseLetter & numbers)
            {
                bool checkCommonPass = CheckValidationByCommonPasswords(pass);
                if (checkCommonPass)
                {
                    log.Debug("the password is valid");
                    return true;
                }
                else
                {
                    log.Debug("the password is a common password.");
                    return false;
                }
            }
            else
            {
                log.Warn("The password does not contain any of the following conditions: number, uppercase letter, lowercase letter.");
                return false;
            }
        }

        public bool CheckValidationByCommonPasswords(string password)
        {
            foreach (string pass in commonPasswords)
            {
                if (pass.Equals(password))
                    return false;
            }
            return true;
        }
    }
}
