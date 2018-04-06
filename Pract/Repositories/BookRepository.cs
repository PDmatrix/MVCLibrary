using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Pract.App_LocalResources;
using Pract.Interfaces;
using Pract.Models;

namespace Pract.Repositories
{
    public sealed class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly LibContext _db;

        public BookRepository(DbContext db) : base(db)
        {
            _db = new LibContext();
        }

        public PagingViewModel<Book> PageBook(int page)
        {
            return PagingIndex(_db.Books.OrderBy(r => r.Id).AsNoTracking(), page);
        }
    }
}