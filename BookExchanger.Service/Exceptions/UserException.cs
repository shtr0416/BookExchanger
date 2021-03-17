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

        public string ErrorMsgConvert()
        {
            switch (Type)
            {
                case ExceptionType.UserExists:
                    return "User exists";

                case ExceptionType.UserNotFound:
                    return "User not found";

                case ExceptionType.UserNameRequired:
                    return "User name is required";

                case ExceptionType.UserPassRequired:
                    return "User password is required";

                case ExceptionType.UserIdRequired:
                    return "User id is required";

                default:
                    return "UnknowException";
            }
        }
    }
}