using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pract.Models;
using Pract.Server;

namespace Pract.Controllers
{
    public class UsersController : Controller
    {

        // GET: Users
        public ActionResult Index()
        {
            return View(UserHandler.IndexUser());
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (UserHandler.CreateUser(user, ModelState.IsValid))
            {
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = UserHandler.FindUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (UserHandler.EditUser(user, ModelState.IsValid))
            {
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = UserHandler.FindUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(UserHandler.UserView(user));
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserHandler.DeleteUser(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            LibContext db = new LibContext();
            if (disposing)
            {
               db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
