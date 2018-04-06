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
        IReceiptRepository Receipts { get; }
        IBookRepository Books { get; }
        IUserRepository Users { get; }
    }
}
