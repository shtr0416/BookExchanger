using BookExchanger.Service;
using BookExchangerApi.App;
using BookExchangerApi.Jwt;
using BookExchangerApi.Models;
using BookExchangerApi.Session;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookExchangerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILoginService _loginService;
        private readonly ITokenService _tokenService;
        private readonly IUserSession _userSession;

        public UserController(IUserService userService, ILoginService loginService, ITokenService tokenService, IUserSession userSession)
        {
            this._userService = userService;
            this._loginService = loginService;
            this._tokenService = tokenService;
            this._userSession = userSession;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<BookExchanger.Service.Dto.User.Get.ResponseDto> Get(string id)
        {
            IUserService userService = new BookExchanger.Service.Services.UserService();
            var user = await userService.GetAsync(id, true);

            return user;
        }

        // POST api/<UserController>
        [HttpPost]
        [AllowAnonymous]
        public async Task<BaseResponse<string>> Post()
        {
            IUserService userService = new BookExchanger.Service.Services.UserService();
            try
            {
                var userId = await userService.CreateAsync(new BookExchanger.Service.Dto.User.Create.RequestDto()
                {
                    Address = "ADDRESS",
                    City = "CITY",
                    Country = "COUNTRY",
                    Degree = "DEGREE",
                    Description = "DESCRIPTION",
                    District = "DISTRICT",
                    NickName = "NICKNAME",
                    Password = "PASSWORD",
                    Phone = "PHONE",
                    Province = "PROVINCE",
                    UserName = "FREDTU"
                });

                return new BaseResponse<string>(userId);
            }
            catch (BookExchanger.Service.Exceptions.UserException ex)
            {
                return new BaseResponse<string>(500, ex.ErrorMsgConvert());
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>(500, ex.Message);
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<BaseResponse<TokenEntity>> Login([FromBody] Models.User.LoginRequest loginRequest)
        {
            try
            {
                var user = await _loginService.Login((loginRequest.UserName, loginRequest.Password));

                if (user is null)
                    return new BaseResponse<TokenEntity>()
                    {
                        StatusCode = (int)HttpStatusCode.Unauthorized,
                        Message = "用户名密码错误",
                        Data = null
                    };

                Dictionary<string, string> userInfos = new Dictionary<string, string>();
                userInfos.Add(nameof(user.UserId), user.UserId);
                userInfos.Add(nameof(user.NickName), user.NickName);
                userInfos.Add(nameof(user.Country), user.Country);
                userInfos.Add(nameof(user.LastSignAt), user.LastSignAt.Value.ToString("yyy-MM-dd HH:mm:ss"));
                userInfos.Add(nameof(user.Address), $"{user.Country} {user.Province} {user.City} {user.District} {user.Address}");

                var tokenInfo = _tokenService.GetToken(userInfos);
                return new BaseResponse<TokenEntity>()
                {
                    StatusCode = 200,
                    Message = "",
                    Data = tokenInfo
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<TokenEntity>()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = $"登录失败: {ex.Message}",
                    Data = null
                };
            }
        }
    }
}