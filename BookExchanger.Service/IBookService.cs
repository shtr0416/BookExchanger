using BookExchanger.Service.Dto.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookExchanger.Service
{
    public interface IBookService
    {
        Task<BookDto> Create();

        Task Update();

        Task Delete();
    }
}