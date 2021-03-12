using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookExchanger.Service.Dto.User.Update
{
    public class RequestDto
    {
        public string UserId { get; set; }
        public string NickName { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public string Degree { get; set; }

        public void CombineToDbEntity(ref Application.User user)
        {
            user.NickName = NickName;
            user.Country = Country;
            user.Province = Province;
            user.Address = Address;
            user.City = City;
            user.Degree = Degree;
            user.District = District;
            user.Phone = Phone;
            user.Description = Description;
        }
    }
}