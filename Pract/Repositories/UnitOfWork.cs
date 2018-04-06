using System;
using Pract.Interfaces;
using Pract.Models;

namespace Pract.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibContext _db;
        private BookRepository _bookRepository;
        private ReceiptRepository _receiptRepository;
        private UserRepository _userRepository;

        public UnitOfWork()
        {
            _db = new LibContext();
        }

        public IBookRepository Books => _bookRepository ?? (_bookRepository = new BookRepository(_db));

        public IReceiptRepository Receipts => _receiptRepository ?? (_receiptRepository = new ReceiptRepository(_db));

        public IUserRepository Users => _userRepository ?? (_userRepository = new UserRepository(_db));

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