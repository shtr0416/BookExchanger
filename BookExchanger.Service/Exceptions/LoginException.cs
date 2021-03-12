using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookExchanger.Service.Exceptions
{
    public class LoginException : Exception
    {
        public LoginExceptionType Type { get; set; }

        public LoginException(string message, LoginExceptionType type) : base(message)
        {
            this.Type = type;
        }

        public enum LoginExceptionType
        {
            UserNameRequired,
            UserPassRequired,
            UserNotExists,
            PasswordInvalid
        }
    }
}