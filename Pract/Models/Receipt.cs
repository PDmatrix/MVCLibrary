using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Pract.Models
{
    public class Receipt
    {
        public int Id { get; set; }
        [Display(Name = "ФИО")]
        public int UserId { get; set; }
        [Display(Name = "Название книги")]
        public int BookId { get; set; }
        [Display(Name = "Дата выдачи")]
        [DataType(DataType.Date)]
        //Для DatePicker
        //[DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)] 
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] 
        public DateTime Date { get; set; }
        public User User { get; set; }
        public Book Book { get; set; }
    }
}