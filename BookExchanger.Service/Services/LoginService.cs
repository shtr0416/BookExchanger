using BookExchanger.Service.Dto.User.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookExchanger.Service.Services
{
    public class LoginService : ILoginService
    {
        private readonly UserService _userService;

        public LoginService()
        {
            _userService = new UserService();
        }

        public async Task<bool> Login(string userName, string userPassword)
        {
            if (string.IsNullOrEmpty(userName))
                throw new Exceptions.LoginException(nameof(userName), Exceptions.LoginException.LoginExceptionType.UserNameRequired);
            if (string.IsNullOrEmpty(userPassword))
                throw new Exceptions.LoginException(nameof(userPassword), Exceptions.LoginException.LoginExceptionType.UserPassRequired);

            var user = await _userService.GetAsync(
                condition: new KeyValuePair<UserService.GetFuncCondtionType, string>(UserService.GetFuncCondtionType.UserName, userName),
                isActive: true);

            if (user is null || string.IsNullOrEmpty(user.UserId))
                return false;

            var originalPassword = user.UserPass;
            var salt = user.Salt;

            var isConfirmed = Auth.Service.PasswordService.Confirm(userPassword, originalPassword, salt);

            if (!isConfirmed)
                return false;

            return true;
        }

        public async Task<ResponseDto> Login((string uname, string upass) loginInfo)
        {
            if (string.IsNullOrEmpty(loginInfo.uname))
                throw new Exceptions.LoginException(nameof(loginInfo.uname), Exceptions.LoginException.LoginExceptionType.UserNameRequired);
            if (string.IsNullOrEmpty(loginInfo.upass))
                throw new Exceptions.LoginException(nameof(loginInfo.upass), Exceptions.LoginException.LoginExceptionType.UserPassRequired);

            var user = await _userService.GetAsync(
                condition: new KeyValuePair<UserService.GetFuncCondtionType, string>(UserService.GetFuncCondtionType.UserName, loginInfo.uname),
                isActive: true);

            if (user is null || string.IsNullOrEmpty(user.UserId))
                return null;

            var originalPassword = user.UserPass;
            var salt = user.Salt;

            var isConfirmed = Auth.Service.PasswordService.Confirm(loginInfo.upass, originalPassword, salt);

            if (!isConfirmed)
                return null;

            var response = ResponseDto.FromDbEntity(user);

            return response;
        }

        public async Task<string> Register(Dto.User.Create.RequestDto request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(Dto.User.Create.RequestDto));
            var userId = await _userService.CreateAsync(request);

            return userId;
        }
    }
}