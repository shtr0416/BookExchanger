using BookExchanger.Application;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookExchanger.Service.Services
{
    public class BaseService<T> : DbContext where T : class
    {
        public BookExchangerContext DbContext { get; private set; }
        public DbSet<T> DbEntity { get; private set; }

        public BaseService()
        {
            DbContext = new BookExchangerContext();
            DbEntity = DbContext.Set<T>();
        }
    }
}