using BookExchanger.Service.Dto.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookExchanger.Service.Dto.Book
{
    public class BookDto
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public CategoryDto Category { get; set; }

        public List<string> Tags { get; set; }
        public string Level { get; set; }
        public DateTime? PublishedAt { get; set; }
    }
}