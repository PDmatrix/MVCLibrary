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
    public class ReceiptsController : Controller
    {

        private readonly ReceiptRepository _db;

        public ReceiptsController()
        {
            _db = new ReceiptRepository(new LibContext());
        }

        // GET: Receipts
        public ActionResult Index(int page = 1)
        {
            return View(_db.PageReceipt(page > 0 ? page : 1));
        }

        // GET: Receipts/Create
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Create()
        {
            return View(_db.ReceiptCreateView());
        }

        // POST: Receipts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Create([Bind(Exclude = "User, Book, PageInfo")] Receipt receipt)
        {
            if (ModelState.IsValid)
            {
                _db.Create(receipt);
                return RedirectToAction("Index");
            }
            return View(_db.ReceiptView(receipt));
        }

        // GET: Receipts/Edit/5
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receipt receipt = _db.Find(id);
            if (receipt == null)
            {
                return HttpNotFound();
            }
            return View(_db.ReceiptView(receipt));
        }

        // POST: Receipts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Edit([Bind(Exclude = "User, Book, PageInfo")] Receipt receipt)
        {
            if (ModelState.IsValid)
            {
                _db.Update(receipt);
                return RedirectToAction("Index");
            }
            return View(_db.ReceiptView(receipt));
        }

        // GET: Receipts/Delete/5
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receipt receipt =_db.FindReceiptInclude(id);
            if (receipt == null)
            {
                return HttpNotFound();
            }
            return View(receipt);
        }

        // POST: Receipts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult DeleteConfirmed(int id)
        {
            _db.Delete(id);
            return RedirectToAction("Index");
        }

        // GET: Receipts/Overdue
        public ActionResult Overdue(int page = 1)
        {
            return View(_db.OverdueReceipt(page > 0 ? page : 1));
        }
    }
}
