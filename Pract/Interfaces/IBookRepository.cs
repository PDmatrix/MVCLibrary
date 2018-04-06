using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pract.Models;

namespace Pract.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        PagingViewModel<Book> PageBook(int page);
    }
}
