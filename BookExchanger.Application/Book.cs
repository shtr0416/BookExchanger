using System;
using System.Collections.Generic;

#nullable disable

namespace BookExchanger.Application
{
    public partial class Book
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public int? CategoryId { get; set; }
        public string Tags { get; set; }
        public string Level { get; set; }
        public string Creator { get; set; }
        public DateTime? PublishedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        public virtual Category Category { get; set; }
        public virtual User CreatorNavigation { get; set; }
    }
}
