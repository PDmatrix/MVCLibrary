using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pract.Models;
using Pract.Repositories;

namespace Pract.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ReceiptRepository Receipts { get; }
        BookRepository Books { get; }
        UserRepository Users { get; }
    }
}
