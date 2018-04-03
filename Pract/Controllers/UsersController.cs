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
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserRepository _db;

        public UsersController()
        {
            _db = new UserRepository(new LibContext());
        }

        // GET: Users
        public ActionResult Index(int page = 1)
        {

            return View(_db.PageUser(page > 0 ? page : 1));
        }

        // GET: Users/Create
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _db.Create(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Edit/5
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _db.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                _db.Update(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _db.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(_db.FindViewModel(user.Id));
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult DeleteConfirmed(int id)
        {
            _db.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
