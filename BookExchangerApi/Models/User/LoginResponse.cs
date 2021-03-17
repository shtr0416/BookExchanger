using BookExchangerApi.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookExchangerApi.Models.User
{
    public class LoginResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public TokenEntity Token { get; set; }
    }
}