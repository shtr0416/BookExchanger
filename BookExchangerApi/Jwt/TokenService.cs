using BookExchanger.Service.Dto.User.Get;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BookExchangerApi.Jwt
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<JwtConfig> _jwtConfig;

        public TokenService(IOptions<JwtConfig> jwtConfig)
        {
            _jwtConfig = jwtConfig;
        }

        public TokenEntity GetToken(ResponseDto user)
        {
            var claims = new List<Claim>()
                 {
                     new Claim(nameof(user.UserId), user.UserId),
                     new Claim(nameof(user.NickName), user.NickName),
                     new Claim(nameof(user.LastSignAt), user.LastSignAt.Value.ToString("yyyy-MM-dd HH:mm:ss")),
                     new Claim(nameof(user.Country), user.Country),
                     new Claim(nameof(user.Address), $"{user.Country} {user.Province} {user.City} {user.District} {user.Address}")
                 };

            return this.CreateTokenString(claims);
        }

        public TokenEntity GetToken(Dictionary<string, string> dict)
        {
            var claims = dict.Select(item =>
            {
                return new Claim(item.Key, item.Value);
            });

            return CreateTokenString(claims.ToList());
        }

        public bool ValidToken(string token, out Dictionary<string, string> info)
        {
            info = null;
            bool success = true;
            if (!token.Contains("."))
                return false;

            var jwtArr = token.Split(".");
            if (jwtArr.Length < 3)
                return false;

            string headerStr = jwtArr[0].Replace("Bearer ", "");

            var header = JsonConvert.DeserializeObject<Dictionary<string, string>>(Base64UrlEncoder.Decode(headerStr));
            var payLoad = JsonConvert.DeserializeObject<Dictionary<string, string>>(Base64UrlEncoder.Decode(jwtArr[1]));
            var hs256 = new HMACSHA256(Encoding.ASCII.GetBytes(_jwtConfig.Value.IssuerSigningKey));
            success = success && string.Equals(jwtArr[2], Base64UrlEncoder.Encode(hs256.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(headerStr, ".", jwtArr[1])))));
            if (!success)
                return success;

            var now = ToUnixEpochDate(DateTime.UtcNow);
            success = success && (now >= long.Parse(payLoad["nbf"].ToString()) && now < long.Parse(payLoad["exp"].ToString()));

            info = payLoad;

            return success;
        }

        private long ToUnixEpochDate(DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        }

        /// <summary>
        /// 生成token
        /// </summary>
        /// <param name="claims">List的 Claim对象</param>
        /// <returns></returns>
        private TokenEntity CreateTokenString(List<Claim> claims)
        {
            var now = DateTime.Now;
            var expires = now.Add(TimeSpan.FromMinutes(_jwtConfig.Value.AccessTokenExpiresMinutes));
            var token = new JwtSecurityToken(
                issuer: _jwtConfig.Value.Issuer,//Token发布者
                audience: _jwtConfig.Value.Audience,//Token接受者
                claims: claims,//携带的负载
                notBefore: now,//当前时间token生成时间
                expires: expires,//过期时间
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Value.IssuerSigningKey)), SecurityAlgorithms.HmacSha256));
            return new TokenEntity() { Token = new JwtSecurityTokenHandler().WriteToken(token), Expires = expires };
        }
    }
}