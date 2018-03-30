using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pract.Models
{
    public class RoleManagementViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        [Required(ErrorMessage = "Введите роль")]
        [Display(Name = "Роль")]
        public string Name { get; set; }

    }
}