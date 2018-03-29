using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pract.Models
{
    public class BookPagingViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}