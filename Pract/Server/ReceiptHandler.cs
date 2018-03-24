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
    public class ReceiptHandler
    {
        private static LibContext db = new LibContext();

        public static IEnumerable<Receipt> IndexReceipt()
        {
            var list = db.Receipts.Include(r => r.Book).Include(r => r.User).ToArray();
            return list;
        }

        public static ReceiptViewModel ReceiptCreateView()
        {
            ReceiptViewModel viewModel = new ReceiptViewModel
            {
                Users = new SelectList(db.Users.ToArray(), "Id", "Name"),
                Books = new SelectList(db.Books.ToArray(), "Id", "Name"),
                Date = null
            };
            return viewModel;
        }

        public static ReceiptViewModel ReceiptView(Receipt receipt)
        {
            ReceiptViewModel viewModel = new ReceiptViewModel
            {
                Id = receipt.Id,
                Users = new SelectList(db.Users.ToArray(), "Id", "Name", receipt.User),
                Books = new SelectList(db.Books.ToArray(), "Id", "Name", receipt.Date),
                Date = receipt.Date
            };
            return viewModel;
        }

        public static Receipt FindReceipt(int? id)
        {
            return db.Receipts.Find(id);
        }

        public static Receipt FindReceiptInclude(int? id)
        {
            return db.Receipts.Include(r => r.Book).Include(r => r.User).FirstOrDefault(r => r.Id == id);
        }

        public static bool CreateReceipt(Receipt receipt, bool isValid)
        {
            if (isValid)
            {
                db.Receipts.Add(receipt);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public static bool EditReceipt(Receipt receipt, bool isValid)
        {
            if (isValid)
            {
                var local = db.Set<Receipt>()
                         .Local
                         .FirstOrDefault(f => f.Id == receipt.Id);
                if (local != null)
                {
                    db.Entry(local).State = EntityState.Detached;
                }
                db.Entry(receipt).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public static void DeleteReceipt(int id)
        {
            Receipt receipt = FindReceipt(id);
            db.Receipts.Remove(receipt);
            db.SaveChanges();
        }

        public static IEnumerable<Receipt> OverdueReceipt()
        {
            var list = db.Receipts.Where(r => r.Date <= DateTime.Now).Include(r => r.Book).Include(r => r.User).ToArray();
            return list;
        }
    }
}