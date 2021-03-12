using BookExchanger.Application;
using BookExchanger.Service.Dto.User.Get;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BookExchanger.Service.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<string> CreateAsync(Dto.User.Create.RequestDto user)
        {
            if (string.IsNullOrEmpty(user.Password))
                throw new Exceptions.UserException(nameof(user.UserName), Exceptions.UserException.ExceptionType.UserPassRequired);
            if (string.IsNullOrEmpty(user.UserName))
                throw new Exceptions.UserException(nameof(user.Password), Exceptions.UserException.ExceptionType.UserNameRequired);

            var isExists = await Exists(user.UserName);
            if (isExists)
                throw new Exceptions.UserException(message: "UserName", type: Exceptions.UserException.ExceptionType.UserExists);

            var userEntity = user.ToDbEntity();

            await base.DbEntity.AddAsync(userEntity);

            int iRow = await DbContext.SaveChangesAsync();
            if (iRow <= 0)
                return string.Empty;

            return userEntity.UserId;
        }

        public async Task<bool> DeleteAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new Exceptions.UserException(nameof(userId), Exceptions.UserException.ExceptionType.UserIdRequired);

            var user = await DbEntity.SingleOrDefaultAsync(a => a.UserId == userId);
            if (user is null || string.IsNullOrEmpty(user.UserId))
                throw new Exceptions.UserException(userId, Exceptions.UserException.ExceptionType.UserNotFound);

            if (user.Books is not null and { Count: > 0 })
            {
                var bookDbSet = base.Set<Book>();
                bookDbSet.RemoveRange(user.Books);
            }

            this.DbEntity.Remove(user);

            var iRow = await this.SaveChangesAsync();
            if (iRow <= 0)
                return false;

            return true;
        }

        public async Task<bool> Exists(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException("UserName");

            var iCount = await this.DbEntity.CountAsync(a => a.UserName == userName);
            if (iCount > 0)
                return true;

            return false;
        }

        public async Task<Dto.User.Get.ResponseDto> GetAsync(string userId, bool? isActive = true)
        {
            User user = await GetAsync(new KeyValuePair<GetFuncCondtionType, string>(GetFuncCondtionType.UserId, userId), isActive);
            return Dto.User.Get.ResponseDto.FromDbEntity(user);
        }

        public async Task<Dto.User.Get.ResponseDto> GetAsync(Dto.User.Get.RequestDto request)
        {
            if (!string.IsNullOrEmpty(request.UserId))
                return await GetAsync(request.UserId, request.IsActive);

            if (string.IsNullOrEmpty(request.UserName))
                throw new Exceptions.UserException(nameof(request.UserName), Exceptions.UserException.ExceptionType.UserNameRequired);

            User user = await GetAsync(new KeyValuePair<GetFuncCondtionType, string>(GetFuncCondtionType.UserName, request.UserName), request.IsActive);

            return Dto.User.Get.ResponseDto.FromDbEntity(user);
        }

        internal async Task<User> GetAsync(KeyValuePair<GetFuncCondtionType, string> condition, bool? isActive)
        {
            User user = null;

            if (condition.Key == GetFuncCondtionType.UserName)
            {
                if (!isActive.HasValue)
                    user = await this.DbEntity.SingleOrDefaultAsync(a => a.UserName == condition.Value);
                else
                    user = await this.DbEntity.SingleOrDefaultAsync(a => a.UserName == condition.Value && a.IsActive == isActive.Value);
            }
            else if (condition.Key == GetFuncCondtionType.UserId)
            {
                if (!isActive.HasValue)
                    user = await this.DbEntity.SingleOrDefaultAsync(a => a.UserId == condition.Value);
                else
                    user = await this.DbEntity.SingleOrDefaultAsync(a => a.UserId == condition.Value && a.IsActive == isActive.Value);
            }
            else
                throw new NotSupportedException(condition.Key.ToString());

            return user;
        }

        internal enum GetFuncCondtionType
        {
            UserId,
            UserName
        }

        public async Task<bool> UpdateAsync(Dto.User.Update.RequestDto user)
        {
            if (string.IsNullOrEmpty(user.UserId))
                throw new Exceptions.UserException(nameof(user.UserId), Exceptions.UserException.ExceptionType.UserIdRequired);

            var userEntity = await this.DbEntity.SingleOrDefaultAsync(a => a.UserId == user.UserId);
            if (userEntity is null || string.IsNullOrEmpty(userEntity.UserId))
                throw new Exceptions.UserException(nameof(user.UserId), Exceptions.UserException.ExceptionType.UserNotFound);

            user.CombineToDbEntity(ref userEntity);

            this.DbEntity.Update(userEntity);

            var iRow = await this.SaveChangesAsync();
            if (iRow <= 0)
                return false;

            return true;
        }
    }
}