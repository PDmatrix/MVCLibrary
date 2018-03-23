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
        [Required]
        [Display(Name = "ФИО")]
        public int UserId { get; set; }
        [Required]
        [Display(Name = "Название книги")]
        public int BookId { get; set; }
        [Required]
        [Display(Name = "Дата выдачи")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)] 
        public DateTime Date { get; set; }
        public User User { get; set; }
        public Book Book { get; set; }
    }
}