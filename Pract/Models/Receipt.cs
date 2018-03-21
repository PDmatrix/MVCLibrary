using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pract.Models
{
    public class Receipt
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        //[DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        //public DateTime Date { get; set; }
        public User User { get; set; }
        public Book Book { get; set; }
    }
}