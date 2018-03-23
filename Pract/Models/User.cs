using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Pract.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "ФИО")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Date)]
        //Для DatePicker
        //[DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)] 
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата рождения")]
        public DateTime Birthday { get; set; }
    }
}