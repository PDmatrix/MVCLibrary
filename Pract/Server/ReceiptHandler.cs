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
    public static class ReceiptHandler
    {
        private static readonly LibContext db = new LibContext();
        private static readonly int PageSize = Convert.ToInt32(Properties.Resources.PageSize);


        private static ReceiptPagingViewModel PagingIndex(IQueryable<Receipt> receipts, int page)
        {
            IEnumerable<Receipt> receiptsPerPages= receipts.OrderBy(r => r.Id).Skip((page - 1) * PageSize).Take(PageSize);
            PageInfo pageInfo = new PageInfo { PageNumber=page, TotalItems= receipts.Count()};
            return new ReceiptPagingViewModel { PageInfo = pageInfo, Receipts = receiptsPerPages };
        }

        public static ReceiptPagingViewModel IndexReceipt(int page)
        {
            return PagingIndex(db.Receipts.Include(r => r.Book).Include(r => r.User), page);
        }

        public static ReceiptEditViewModel ReceiptCreateView()
        {
            ReceiptEditViewModel viewModel = new ReceiptEditViewModel
            {
                Users = new SelectList(db.Users.ToArray(), "Id", "Name"),
                Books = new SelectList(db.Books.ToArray(), "Id", "Name"),
                Date = null,
                DateReturn = null
            };
            return viewModel;
        }

        public static ReceiptEditViewModel ReceiptView(Receipt receipt)
        {
            ReceiptEditViewModel viewModel = new ReceiptEditViewModel
            {
                Id = receipt.Id,
                Users = new SelectList(db.Users.ToArray(), "Id", "Name", receipt.User.Id),
                Books = new SelectList(db.Books.ToArray(), "Id", "Name", receipt.Book.Id),
                Date = receipt.Date,
                DateReturn = receipt.DateReturn
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

        public static void CreateReceipt(Receipt receipt)
        {
            db.Receipts.Add(receipt);
            db.SaveChanges();
        }

        public static void EditReceipt(Receipt receipt)
        {
            var local = db.Set<Receipt>().Local.FirstOrDefault(f => f.Id == receipt.Id);
            if (local != null)
            {
                db.Entry(local).State = EntityState.Detached;
            }

            db.Entry(receipt).State = EntityState.Modified;
            db.SaveChanges();
        }

        public static void DeleteReceipt(int id)
        {
            Receipt receipt = FindReceipt(id);
            db.Receipts.Remove(receipt);
            db.SaveChanges();
        }

        public static ReceiptPagingViewModel OverdueReceipt(int page)
        {
            return PagingIndex( db.Receipts.Where(r => r.DateReturn <= DateTime.Now).Include(r => r.Book).Include(r => r.User), page);
        }
    }
}