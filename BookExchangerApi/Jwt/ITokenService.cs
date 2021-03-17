using BookExchanger.Service.Dto.User.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookExchangerApi.Jwt
{
    public interface ITokenService
    {
        TokenEntity GetToken(ResponseDto user);

        TokenEntity GetToken(Dictionary<string, string> dict);

        bool ValidToken(string token, out Dictionary<string, string> info);
    }
}