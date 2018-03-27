using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Pract.Models
{
    public class ApplicationContext : IdentityDbContext<ApplicationAdmin>
    {
        public ApplicationContext() : base("LibContext") { }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }
    }
}