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
        private readonly UnitOfWork _unitOfWork;

        public ReceiptsController()
        {
            _unitOfWork = new UnitOfWork();
        }

        // GET: Receipts
        public ActionResult Index(int page = 1)
        {
            return View(_unitOfWork.Receipts.PageReceipt(page > 0 ? page : 1));
        }

        // GET: Receipts/Create
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Create()
        {
            return View(_unitOfWork.Receipts.ReceiptCreateView());
        }

        // POST: Receipts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Create([Bind(Exclude = "User, Book, PageInfo")] Receipt receipt)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Receipts.Create(receipt);
                return RedirectToAction("Index");
            }
            return View(_unitOfWork.Receipts.ReceiptView(receipt));
        }

        // GET: Receipts/Edit/5
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receipt receipt = _unitOfWork.Receipts.Find(id);
            if (receipt == null)
            {
                return HttpNotFound();
            }
            return View(_unitOfWork.Receipts.ReceiptView(receipt));
        }

        // POST: Receipts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Edit([Bind(Exclude = "User, Book, PageInfo")] Receipt receipt)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Receipts.Update(receipt);
                return RedirectToAction("Index");
            }
            return View(_unitOfWork.Receipts.ReceiptView(receipt));
        }

        // GET: Receipts/Delete/5
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receipt receipt =_unitOfWork.Receipts.FindReceiptInclude(id);
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
            _unitOfWork.Receipts.Delete(id);
            return RedirectToAction("Index");
        }

        // GET: Receipts/Overdue
        public ActionResult Overdue(int page = 1)
        {
            return View(_unitOfWork.Receipts.OverdueReceipt(page > 0 ? page : 1));
        }
    }
}
