﻿using System;
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

        public async Task<string> Register(Dto.User.Create.RequestDto request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(Dto.User.Create.RequestDto));
            var userId = await _userService.CreateAsync(request);

            return userId;
        }
    }
}