using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookExchanger.Service.Dto.User.Get
{
    public class RequestDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool? IsActive { get; set; }
    }
}