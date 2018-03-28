using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Pract.Models
{
    public class UserIndexViewModel
    {
        public User Users { get; set; }

        [Required(ErrorMessage = "Введите дату рождения")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата рождения")]
        public DateTime Birthday { get; set; }

        [Display(Name = "Возраст")]
        public int Age
        {
            get
            {
                return (DateTime.Now.Month < Birthday.Month ||
                        (DateTime.Now.Month == Birthday.Month && DateTime.Now.Day < Birthday.Day))
                    ? DateTime.Now.Year - Birthday.Year - 1
                    : DateTime.Now.Year - Birthday.Year;
            }
        }
    }
}