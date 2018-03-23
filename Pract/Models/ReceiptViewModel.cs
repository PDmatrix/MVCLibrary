using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Pract.Models
{
    public class ReceiptViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Дата выдачи")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }
        [Required]
        [Display(Name = "Название книги")]
        public SelectList Books { get; set; }
        [Required]
        [Display(Name = "ФИО")]
        public SelectList Users { get; set; }
    }
}