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
        private async Task CreateAdmin()
        {
            var admin = await FindByNameAsync("admin");
            if (admin == null)
            {
                var newAdmin = new ApplicationUser { UserName = "admin" };
                var result = await CreateAsync(newAdmin, "admin1");
            }          
        }

        private ApplicationUserManager(IUserStore<ApplicationUser> store)
                : base(store)
        {

        }
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
                                                IOwinContext context)
        {
            ApplicationContext db = context.Get<ApplicationContext>();
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            return manager;
        }
    }
}