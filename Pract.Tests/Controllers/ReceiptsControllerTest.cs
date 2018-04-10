using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pract.Controllers;
using Pract.Repositories;
using System.Data.Entity;
using Moq;
using Pract.Interfaces;
using Pract.Models;

namespace Pract.Tests.Controllers
{
    [TestClass]
    public class ReceiptsControllerTest
    {
        private ReceiptsController _controller;
        private ViewResult _result;
        private readonly Mock<IUnitOfWork> _mock = new Mock<IUnitOfWork>();
        [TestMethod]
        public void Index()
        {

            _mock.Setup(a => a.Receipts.PageReceipt(1)).Returns(new PagingViewModel<Receipt>());
            _controller = new ReceiptsController(_mock.Object);
 
            // Act
            _result = _controller.Index() as ViewResult;
 
            // Assert
            Assert.IsNotNull(_result.Model);
        }

        [TestMethod]
        public void Create()
        {
            _mock.Setup(a => a.Receipts.ReceiptCreateView()).Returns(new ReceiptEditViewModel());
            _controller = new ReceiptsController(_mock.Object);
 
            // Act
            _result = _controller.Create() as ViewResult;
 
            // Assert
            Assert.IsNotNull(_result);
        }

        [TestMethod]
        public void Edit()
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
            _mock.Setup(a => a.Receipts.ReceiptView(receipt)).Returns(new ReceiptEditViewModel());
            _controller = new ReceiptsController(_mock.Object);
 
            // Act
            _result = _controller.Edit(1) as ViewResult;
 
            // Assert
            _mock.Verify();

        }

        [TestMethod]
        public void Delete()
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
            _mock.Setup(a => a.Receipts.ReceiptView(receipt)).Returns(new ReceiptEditViewModel());
            _controller = new ReceiptsController(_mock.Object);
 
            // Act
            _result = _controller.Delete(1) as ViewResult;
 
            // Assert
            _mock.Verify();
        }

        [TestMethod]
        public void Overdue()
        {
            _mock.Setup(a => a.Receipts.OverdueReceipt(1)).Returns(new PagingViewModel<Receipt>());
            _controller = new ReceiptsController(_mock.Object);
 
            // Act
            _result = _controller.Overdue() as ViewResult;
 
            // Assert
            Assert.IsNotNull(_result);
        }
    }
}
