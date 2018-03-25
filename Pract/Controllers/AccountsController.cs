using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Pract.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;

namespace Pract.Controllers
{
    public class AccountsController : Controller
    {

        private ApplicationAdminManager AdminManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationAdminManager>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public async Task<ActionResult> Login(string returnUrl)
        {
            if(AuthenticationManager.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Receipts");
            }
            ViewBag.returnUrl = returnUrl;
            ApplicationAdmin lastAdmin = await AdminManager.FindByNameAsync("admin");
            IdentityResult deleteResult = await AdminManager.DeleteAsync(lastAdmin);
            if(deleteResult.Succeeded)
            {
                ApplicationAdmin user = new ApplicationAdmin { AdminName = "admin", UserName = "admin" };
                IdentityResult result = await AdminManager.CreateAsync(user, "admin1");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                ApplicationAdmin user = await AdminManager.FindAsync(model.AdminName, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    ClaimsIdentity claim = await AdminManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(claim);
                    if (String.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Receipts");
                    return Redirect(returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }
    }
}