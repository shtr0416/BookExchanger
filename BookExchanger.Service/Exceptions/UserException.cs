using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookExchanger.Service.Exceptions
{
    public class UserException : Exception
    {
        public ExceptionType Type { get; set; }

        public UserException(string message, ExceptionType type) : base(message)
        {
            base.Data.Add("ExceptionType", type);
            this.Type = type;
        }

        public enum ExceptionType
        {
            UserExists,
            UserNotFound,
            UserNameRequired,
            UserPassRequired,
            UserIdRequired
        }
    }
}