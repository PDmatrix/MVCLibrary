using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Pract.Models
{
    public class UserViewModel
    {
        public User Users { get; set; }

        [Required(ErrorMessage = "Введите дату рождения")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата рождения")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Введите возраст")]
        [Display(Name = "Возраст")]
        public int Age { get; set; }
    }
}