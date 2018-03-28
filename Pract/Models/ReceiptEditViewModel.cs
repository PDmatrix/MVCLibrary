using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Pract.Models
{
    public class ReceiptEditViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите дату выдачи")]
        [Display(Name = "Дата выдачи")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }

        [Required(ErrorMessage = "Введите дату возврата")]
        [Display(Name = "Дата возврата")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateReturn { get; set; }

        [Required(ErrorMessage = "Выберите книгу")]
        [Display(Name = "Название книги")]
        public SelectList Books { get; set; }

        [Required(ErrorMessage = "Выберите человека")]
        [Display(Name = "ФИО")]
        public SelectList Users { get; set; }
    }
}