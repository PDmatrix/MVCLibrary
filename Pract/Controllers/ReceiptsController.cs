using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pract.Models;

namespace Pract.Controllers
{
    public class ReceiptsController : Controller
    {
        private LibContext db = new LibContext();

        // GET: Receipts
        public ActionResult Index()
        {
            var receipts = db.Receipts.Include(r => r.Book).Include(r => r.User);
            return View(receipts.ToArray());
        }

        // GET: Receipts/Create
        public ActionResult Create()
        {
            ReceiptViewModel viewModel = new ReceiptViewModel
            {
                Users = new SelectList(db.Users.ToArray(), "Id", "Name"),
                Books = new SelectList(db.Books.ToArray(), "Id", "Name"),
                Date = null
            };
            return View(viewModel);
        }

        // POST: Receipts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "User, Book")] Receipt receipt)
        {
            if (ModelState.IsValid)
            {
                db.Receipts.Add(receipt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ReceiptViewModel viewModel = new ReceiptViewModel
            {
                Id = receipt.Id,
                Users = new SelectList(db.Users.ToArray(), "Id", "Name", receipt.User),
                Books = new SelectList(db.Books.ToArray(), "Id", "Name", receipt.Date),
                Date = receipt.Date
            };
            return View(viewModel);
        }

        // GET: Receipts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receipt receipt = db.Receipts.Find(id);
            if (receipt == null)
            {
                return HttpNotFound();
            }
            ReceiptViewModel viewModel = new ReceiptViewModel
            {
                Id = receipt.Id,
                Users = new SelectList(db.Users.ToArray(), "Id", "Name", receipt.User),
                Books = new SelectList(db.Books.ToArray(), "Id", "Name", receipt.Book),
                Date = receipt.Date
            };
            return View(viewModel);
        }

        // POST: Receipts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "User, Book")] Receipt receipt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(receipt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ReceiptViewModel viewModel = new ReceiptViewModel
            {
                Id = receipt.Id,
                Users = new SelectList(db.Users.ToArray(), "Id", "Name", receipt.User),
                Books = new SelectList(db.Books.ToArray(), "Id", "Name", receipt.Book),
                Date = receipt.Date
            };
            return View(viewModel);
        }

        // GET: Receipts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receipt receipt = db.Receipts.Include(r => r.Book).Include(r => r.User).FirstOrDefault(r => r.Id == id);
            if (receipt == null)
            {
                return HttpNotFound();
            }
            return View(receipt);
        }

        // POST: Receipts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Receipt receipt = db.Receipts.Find(id);
            db.Receipts.Remove(receipt);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Receipts/Overdue
        public ActionResult Overdue()
        {
            var receipts = db.Receipts.Where(r => r.Date <= DateTime.Now).Include(r => r.Book).Include(r => r.User);
            return View(receipts);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
