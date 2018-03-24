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
        public static LibContext db = new LibContext();

        public static IEnumerable<Book> IndexBook()
        {
            return db.Books.ToArray();
        }

        public static bool CreateBook(Book book, bool isValid)
        {
            if(isValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public static Book FindBook(int? id)
        {
            return db.Books.Find(id);
        }

        public static bool EditBook(Book book, bool isValid)
        {
            if (isValid)
            {
                var local = db.Set<Book>()
                         .Local
                         .FirstOrDefault(f => f.Id == book.Id);
                if (local != null)
                {
                    db.Entry(local).State = EntityState.Detached;
                }
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public static void DeleteBook(int id)
        {
            Book book = FindBook(id);
            db.Books.Remove(book);
            db.SaveChanges();
        }
    }
}