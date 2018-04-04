using System;
using Pract.Interfaces;
using Pract.Models;

namespace Pract.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibContext _db = new LibContext();
        private BookRepository _bookRepository;
        private ReceiptRepository _receiptRepository;
        private UserRepository _userRepository;

        public BookRepository Books => _bookRepository ?? (_bookRepository = new BookRepository(_db));

        public ReceiptRepository Receipts => _receiptRepository ?? (_receiptRepository = new ReceiptRepository(_db));

        public UserRepository Users => _userRepository ?? (_userRepository = new UserRepository(_db));

        private bool _disposed = false;

        private void Dispose(bool disposing)
        {
            if(!_disposed)
            {
                if(disposing)
                {
                    _db.Dispose();
                }
            }
            _disposed = true;
        }
 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}