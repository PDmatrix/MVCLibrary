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
    public class ApplicationAdminManager : UserManager<ApplicationAdmin>
    {
        public ApplicationAdminManager(IUserStore<ApplicationAdmin> store)
                : base(store)
        {
        }
        public static ApplicationAdminManager Create(IdentityFactoryOptions<ApplicationAdminManager> options,
                                                IOwinContext context)
        {
            ApplicationContext db = context.Get<ApplicationContext>();
            ApplicationAdminManager manager = new ApplicationAdminManager(new UserStore<ApplicationAdmin>(db));
            return manager;
        }
    }
}