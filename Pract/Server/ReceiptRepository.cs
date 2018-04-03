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
    public class ReceiptRepository : GenericRepository<Receipt>
    {

        private static ReceiptPagingViewModel PagingIndex(IQueryable<Receipt> receipts, int page)
        {
            IEnumerable<Receipt> receiptsPerPages= receipts.OrderBy(r => r.Id).Skip((page - 1) * PageSize).Take(PageSize);
            PageInfo pageInfo = new PageInfo { PageNumber=page, TotalItems= receipts.Count()};
            return new ReceiptPagingViewModel { PageInfo = pageInfo, Receipts = receiptsPerPages };
        }

        private readonly LibContext _db;
 
        public ReceiptRepository(LibContext db) : base(db)
        {
            _db = new LibContext();
        }

        public ReceiptPagingViewModel PageReceipt(int page)
        {
            return PagingIndex(_db.Receipts.Include(r => r.Book).Include(r => r.User), page);
        }

        public ReceiptEditViewModel ReceiptCreateView()
        {
            ReceiptEditViewModel viewModel = new ReceiptEditViewModel
            {
                Users = new SelectList(_db.Users.ToArray(), "Id", "Name"),
                Books = new SelectList(_db.Books.ToArray(), "Id", "Name"),
                Date = null,
                DateReturn = null
            };
            return viewModel;
        }

        public ReceiptEditViewModel ReceiptView(Receipt receipt)
        {
            ReceiptEditViewModel viewModel = new ReceiptEditViewModel
            {
                Id = receipt.Id,
                Users = new SelectList(_db.Users.ToArray(), "Id", "Name", receipt.UserId),
                Books = new SelectList(_db.Books.ToArray(), "Id", "Name", receipt.BookId),
                Date = receipt.Date,
                DateReturn = receipt.DateReturn
            };
            return viewModel;
        }

        public Receipt FindReceiptInclude(int? id)
        {
            return _db.Receipts.Include(r => r.Book).Include(r => r.User).FirstOrDefault(r => r.Id == id);
        }

        public ReceiptPagingViewModel OverdueReceipt(int page)
        {
            return PagingIndex(_db.Receipts.Where(r => r.DateReturn <= DateTime.Now).Include(r => r.Book).Include(r => r.User), page);
        }
    }
}