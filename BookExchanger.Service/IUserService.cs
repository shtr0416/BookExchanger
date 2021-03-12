using System;
using System.Threading.Tasks;

namespace BookExchanger.Service
{
    public interface IUserService
    {
        Task<Dto.User.Get.ResponseDto> GetAsync(string userId, bool? isActive);

        Task<Dto.User.Get.ResponseDto> GetAsync(Dto.User.Get.RequestDto request);

        /// <summary>
        /// Response UserId
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<string> CreateAsync(Dto.User.Create.RequestDto user);

        Task<bool> UpdateAsync(Dto.User.Update.RequestDto user);

        Task<bool> DeleteAsync(string userId);

        Task<bool> Exists(string userName);
    }
}