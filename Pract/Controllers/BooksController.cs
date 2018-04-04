using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pract.Interfaces;
using Pract.Models;
using Pract.Repositories;

namespace Pract.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BooksController(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        // GET: Books
        public ActionResult Index(int page = 1)
        {
            return View(_unitOfWork.Books.PageBook(page > 0 ? page : 1));
        }

        // GET: Books/Create
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Books.Create(book);
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = _unitOfWork.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Books.Update(book);
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = _unitOfWork.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult DeleteConfirmed(int? id)
        {
            _unitOfWork.Books.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
