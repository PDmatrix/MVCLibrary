using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace Pract.Models
{
    public class Book
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Название книги")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Автор")]
        public string Author { get; set; }
    }
}