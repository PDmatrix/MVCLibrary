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

        // GET: Receipts
        public ActionResult Index()
        {
            return View(ReceiptHandler.IndexReceipt());
        }

        // GET: Receipts/Create
        public ActionResult Create()
        {
            return View(ReceiptHandler.ReceiptCreateView());
        }

        // POST: Receipts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "User, Book")] Receipt receipt)
        {
            if (ReceiptHandler.CreateReceipt(receipt, ModelState.IsValid))
            {
                return RedirectToAction("Index");
            }
            return View(ReceiptHandler.ReceiptView(receipt));
        }

        // GET: Receipts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receipt receipt = ReceiptHandler.FindReceipt(id);
            if (receipt == null)
            {
                return HttpNotFound();
            }
            return View(ReceiptHandler.ReceiptView(receipt));
        }

        // POST: Receipts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "User, Book")] Receipt receipt)
        {
            if (ReceiptHandler.EditReceipt(receipt, ModelState.IsValid))
            {
                return RedirectToAction("Index");
            }
            return View(ReceiptHandler.ReceiptView(receipt));
        }

        // GET: Receipts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receipt receipt = ReceiptHandler.FindReceiptInclude(id);
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
            ReceiptHandler.DeleteReceipt(id);
            return RedirectToAction("Index");
        }

        // GET: Receipts/Overdue
        public ActionResult Overdue()
        {
            return View(ReceiptHandler.OverdueReceipt());
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
