using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pract.Controllers;
using Pract.Interfaces;
using Pract.Models;
using Pract.Repositories;

namespace Pract.Tests.Repositories
{
    [TestClass]
    public class ReceiptRepositoryTest
    {
        private IReceiptRepository _repository;

        [TestMethod]
        public void PageReceipt()
        {
            _repository = new ReceiptRepository(new LibContext());
            // Act
            var result = _repository.PageReceipt(1);
            // Assert
            Assert.IsNotNull(result.Elems);
        }

        [TestMethod]
        public void ReceiptCreateView()
        {
            _repository = new ReceiptRepository(new LibContext());
            // Act
            var result = _repository.ReceiptCreateView();
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ReceiptView()
        {
            Receipt receipt = new Receipt
            {
                Book = new Book {Author = "Автор", Id = 1, Name = "Name"},
                BookId = 1,
                Date = DateTime.Now,
                UserId = 1,
                DateReturn = DateTime.Now,
                Id = 1,
                User = new User {Birthday = DateTime.Now, Id = 1, Name = "Name"}
            };
            _repository = new ReceiptRepository(new LibContext());
            // Act
            var result = _repository.ReceiptView(receipt);
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void FindReceiptInclude()
        {
            Receipt receipt = new Receipt
            {
                Book = new Book {Author = "Автор", Id = 1, Name = "Name"},
                BookId = 1,
                Date = DateTime.Now,
                UserId = 1,
                DateReturn = DateTime.Now,
                Id = 1,
                User = new User {Birthday = DateTime.Now, Id = 1, Name = "Name"}
            };
            _repository = new ReceiptRepository(new LibContext());
            _repository.Create(receipt);
            // Act
            var result = _repository.FindReceiptInclude(receipt.Id);
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void OverdueReceipt()
        {
            _repository = new ReceiptRepository(new LibContext());
            // Act
            var result = _repository.OverdueReceipt(1);
            // Assert
            Assert.IsNotNull(result);
        }
    }
}
