using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Pract.App_LocalResources;
using Pract.Models;

namespace Pract.Repositories
{
    public sealed class BookRepository : GenericRepository<Book>
    {
        private BookPagingViewModel PagingIndex(IQueryable<Book> books, int page)
        {
            IEnumerable<Book> booksPerPages= books.OrderBy(r => r.Id).Skip((page - 1) * ResourceClass.PageSize).Take(ResourceClass.PageSize);
            PageInfo pageInfo = new PageInfo { PageNumber=page, TotalItems= books.Count()};
            return new BookPagingViewModel { PageInfo = pageInfo, Books = booksPerPages };
        }

        private readonly LibContext _db;

        public BookRepository(DbContext db) : base(db)
        {
            _db = new LibContext();
        }

        public BookPagingViewModel PageBook(int page)
        {
            return PagingIndex(_db.Books.AsNoTracking(), page);
        }
    }
}