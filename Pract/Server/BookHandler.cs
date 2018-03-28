using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Pract.Models;

namespace Pract.Server
{
    public static class BookHandler
    {
        private static readonly LibContext db = new LibContext();

        public static IEnumerable<Book> IndexBook(int page, int pageSize = 8)
        {
            return db.Books.ToArray().ToPagedList(page, pageSize);
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