using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookExchanger.Service
{
    public interface ILoginService
    {
        Task<bool> Login(string userName, string userPassword);

        Task<Dto.User.Get.ResponseDto> Login((string uname, string upass) loginInfo);
    }
}