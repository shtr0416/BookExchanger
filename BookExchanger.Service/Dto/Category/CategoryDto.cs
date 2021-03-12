using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookExchanger.Service.Dto.Category
{
    public class CategoryDto
    {
        public CategoryDto(int cid, string cname)
        {
            this.CategoryId = cid;
            this.CategoryName = cname;
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}