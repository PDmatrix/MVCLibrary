using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pract.Models;

namespace Pract.Server
{
    public static class BookHandler
    {
        private static readonly LibContext db = new LibContext();
        private static readonly int PageSize = Convert.ToInt32(Properties.Resources.PageSize);

        private static BookPagingViewModel PagingIndex(IQueryable<Book> books, int page)
        {
            IEnumerable<Book> booksPerPages= books.OrderBy(r => r.Id).Skip((page - 1) * PageSize).Take(PageSize);
            PageInfo pageInfo = new PageInfo { PageNumber=page, TotalItems= books.Count()};
            return new BookPagingViewModel { PageInfo = pageInfo, Books = booksPerPages };
        }

        public static BookPagingViewModel IndexBook(int page)
        {
            return PagingIndex(db.Books, page);
        }

        public static void CreateBook(Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
        }

        public static Book FindBook(int? id)
        {
            return db.Books.Find(id);
        }

        public static void EditBook(Book book)
        {
            var local = db.Set<Book>().Local.FirstOrDefault(f => f.Id == book.Id);
            if (local != null)
            {
                db.Entry(local).State = EntityState.Detached;
            }
            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();
        }

        public static void DeleteBook(int id)
        {
            Book book = FindBook(id);
            db.Books.Remove(book);
            db.SaveChanges();
        }
    }
}