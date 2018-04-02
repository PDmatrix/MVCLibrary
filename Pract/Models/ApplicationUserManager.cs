using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Pract.Models
{
    public sealed class ApplicationUserManager : UserManager<ApplicationUser>
    {
        private ApplicationUserManager(IUserStore<ApplicationUser> store)
                : base(store)
        {
            
        }
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
                                                IOwinContext context)
        {
            ApplicationContext db = context.Get<ApplicationContext>();
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            var admin = manager.FindByName("admin");
            if (admin == null)
            {
                var newAdmin = new ApplicationUser { UserName = "admin" };
                var result = manager.Create(newAdmin, "123456");
                manager.AddToRole(newAdmin.Id, "Admin");
            }
            else if(!manager.IsInRole(admin.Id, "Admin") || manager.GetRoles(admin.Id).Count > 1)
            {
                manager.RemoveFromRoles(admin.Id, manager.GetRoles(admin.Id).ToArray());
                manager.AddToRole(admin.Id, "Admin");
            }
            return manager;
        }
    }
}