using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Pract.Models
{
    public class ApplicationAdmin : IdentityUser
    {
        public string AdminName { get; set; }
        public ApplicationAdmin()
        {

        }
    }
}