using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookExchangerApi.Jwt
{
    public class TokenEntity
    {
        public TokenEntity()
        {
        }

        public TokenEntity(string token)
        {
            this.Token = token;
        }

        public string Token { get; set; }

        public DateTime Expires { get; set; }
    }
}