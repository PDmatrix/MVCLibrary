﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pract.Models;

namespace Pract.Server
{
    public static class ReceiptHandler
    {
        private static readonly LibContext db = new LibContext();

        public static IEnumerable<Receipt> IndexReceipt()
        {
            return db.Receipts.Include(r => r.Book).Include(r => r.User).ToArray();
        }

        public static ReceiptViewModel ReceiptCreateView()
        {
            ReceiptViewModel viewModel = new ReceiptViewModel
            {
                Users = new SelectList(db.Users.ToArray(), "Id", "Name"),
                Books = new SelectList(db.Books.ToArray(), "Id", "Name"),
                Date = null,
                DateReturn = null
            };
            return viewModel;
        }

        public static ReceiptViewModel ReceiptView(Receipt receipt)
        {
            ReceiptViewModel viewModel = new ReceiptViewModel
            {
                Id = receipt.Id,
                Users = new SelectList(db.Users.ToArray(), "Id", "Name", receipt.User.Id),
                Books = new SelectList(db.Books.ToArray(), "Id", "Name", receipt.Book.Id),
                Date = receipt.Date,
                DateReturn = receipt.DateReturn
            };
            return viewModel;
        }

        public static Receipt FindReceipt(int? id)
        {
            return db.Receipts.Find(id);
        }

        public static Receipt FindReceiptInclude(int? id)
        {
            return db.Receipts.Include(r => r.Book).Include(r => r.User).FirstOrDefault(r => r.Id == id);
        }

        public static void CreateReceipt(Receipt receipt)
        {
            db.Receipts.Add(receipt);
            db.SaveChanges();
        }

        public static void EditReceipt(Receipt receipt)
        {
            var local = db.Set<Receipt>().Local.FirstOrDefault(f => f.Id == receipt.Id);
            if (local != null)
            {
                db.Entry(local).State = EntityState.Detached;
            }

            db.Entry(receipt).State = EntityState.Modified;
            db.SaveChanges();
        }

        public static void DeleteReceipt(int id)
        {
            Receipt receipt = FindReceipt(id);
            db.Receipts.Remove(receipt);
            db.SaveChanges();
        }

        public static IEnumerable<Receipt> OverdueReceipt()
        {
            return db.Receipts.Where(r => r.DateReturn <= DateTime.Now).Include(r => r.Book).Include(r => r.User).ToArray();
        }
    }
}