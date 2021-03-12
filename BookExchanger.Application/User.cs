using System;
using System.Collections.Generic;

#nullable disable

namespace BookExchanger.Application
{
    public partial class User
    {
        public User()
        {
            Books = new HashSet<Book>();
        }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserPass { get; set; }
        public string Salt { get; set; }
        public string NickName { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public string Degree { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastSignAt { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
