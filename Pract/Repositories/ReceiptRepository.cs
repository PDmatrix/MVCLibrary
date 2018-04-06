using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Pract.App_LocalResources;
using Pract.Models;

namespace Pract.Repositories
{
    public class ReceiptRepository : GenericRepository<Receipt>
    {
        private readonly LibContext _db;
 
        public ReceiptRepository(DbContext db) : base(db)
        {
            _db = new LibContext();
        }

        public PagingViewModel<Receipt> PageReceipt(int page)
        {
            return PagingIndex(_db.Receipts.Include(r => r.Book).Include(r => r.User).OrderBy(r => r.Id), page);
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

        public PagingViewModel<Receipt> OverdueReceipt(int page)
        {
            return PagingIndex(_db.Receipts.Where(r => r.DateReturn <= DateTime.Now).Include(r => r.Book).Include(r => r.User).OrderBy(r => r.Id), page);
        }
    }
}