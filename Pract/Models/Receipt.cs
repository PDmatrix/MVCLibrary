using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Pract.Models
{
    public class Receipt
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "ФИО")]
        public int UserId { get; set; }

        [Display(Name = "Название")]
        public int BookId { get; set; }

        [Display(Name = "Дата выдачи")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)] 
        public DateTime Date { get; set; }

        [Display(Name = "Дата возврата")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)] 
        public DateTime DateReturn { get; set; }

        public User User { get; set; }
        public Book Book { get; set; }

    }
}