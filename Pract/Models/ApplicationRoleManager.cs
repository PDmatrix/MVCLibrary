using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Pract.Models
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        private ApplicationRoleManager(RoleStore<ApplicationRole> store)
            : base(store) 
        {}
        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options,
            IOwinContext context)
        {
            var manager = new ApplicationRoleManager(new
                RoleStore<ApplicationRole>(context.Get<ApplicationContext>()));
            var role = manager.FindByName("Admin");
            if (role == null)
            {
                var newRole = new ApplicationRole {Name = "Admin"};
                var result = manager.Create(newRole);
            }
            return manager;
        }
    }
}