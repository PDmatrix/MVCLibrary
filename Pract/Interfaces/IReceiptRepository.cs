using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Pract.Models;

namespace Pract.Interfaces
{
    public interface IReceiptRepository : IGenericRepository<Receipt>
    {
        PagingViewModel<Receipt> PageReceipt(int page);
        ReceiptEditViewModel ReceiptCreateView();
        ReceiptEditViewModel ReceiptView(Receipt receipt);
        Receipt FindReceiptInclude(int? id);
        PagingViewModel<Receipt> OverdueReceipt(int page);
    }
}
