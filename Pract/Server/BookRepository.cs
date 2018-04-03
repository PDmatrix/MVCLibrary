using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static Pract.Server.ResourceClass;
using Pract.Models;

namespace Pract.Server
{
    public sealed class BookRepository : GenericRepository<Book>
    {
        private BookPagingViewModel PagingIndex(IQueryable<Book> books, int page)
        {
            IEnumerable<Book> booksPerPages= books.OrderBy(r => r.Id).Skip((page - 1) * PageSize).Take(PageSize);
            PageInfo pageInfo = new PageInfo { PageNumber=page, TotalItems= books.Count()};
            return new BookPagingViewModel { PageInfo = pageInfo, Books = booksPerPages };
        }

        private readonly LibContext _db;

        public BookRepository(LibContext db) : base(db)
        {
            _db = new LibContext();
        }

        public BookPagingViewModel PageBook(int page)
        {
            return PagingIndex(_db.Books.AsNoTracking(), page);
        }
    }
}