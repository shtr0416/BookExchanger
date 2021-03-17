using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BookExchangerApi.Session.UserSession;

namespace BookExchangerApi.Session
{
    public interface IUserSession
    {
        UserEntity User { get; }
    }
}