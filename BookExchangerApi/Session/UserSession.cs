using BookExchanger.Service.Dto.User.Get;
using BookExchangerApi.Jwt;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace BookExchangerApi.Session
{
    public class UserSession : IUserSession
    {
        private IHttpContextAccessor _accessor;
        private ITokenService _tokenService;

        public UserSession(IHttpContextAccessor httpContextAccessor, ITokenService tokenService)
        {
            _accessor = httpContextAccessor;
            _tokenService = tokenService;
        }

        public UserEntity User
        {
            get
            {
                var token = _accessor.HttpContext.Request.Headers["Authorization"];
                if (string.IsNullOrEmpty(token))
                    return null;

                Dictionary<string, string> claims = null;
                _tokenService.ValidToken(token, out claims);

                if (claims is null || claims.Count == 0 || string.IsNullOrEmpty(claims[nameof(ResponseDto.UserId)]))
                    return null;

                return new UserEntity(UserId: claims[nameof(ResponseDto.UserId)],
                                NickName: claims[nameof(ResponseDto.NickName)],
                                Address: claims[nameof(ResponseDto.Address)],
                                LastSignAt: claims[nameof(ResponseDto.LastSignAt)]);
            }
        }

        public record UserEntity(string UserId, string NickName, string Address, string LastSignAt);
    }
}