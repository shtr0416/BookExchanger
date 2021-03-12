using BookExchanger.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookExchanger.Service.Dto.User.Create
{
    /// <summary>
    /// User create request model
    /// </summary>
    public class RequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public string Degree { get; set; }

        public Application.User ToDbEntity()
        {
            var salt = Auth.Service.PasswordService.GetSalt();
            var password = Auth.Service.PasswordService.Encryption(this.Password, salt);

            return new Application.User()
            {
                Address = Address,
                Books = null,
                City = City,
                Country = Country,
                CreatedAt = DateTime.Now,
                Degree = Degree,
                Description = Description,
                District = District,
                IsActive = true,
                LastSignAt = DateTime.Now,
                NickName = NickName,
                Phone = Phone,
                Province = Province,
                Salt = salt,
                UserId = Guid.NewGuid().ToString(),
                UserName = UserName,
                UserPass = password
            };
        }
    }
}