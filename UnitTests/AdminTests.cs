using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sportore.Domain.Abstract;
using Sportore.Domain.Entities;
using Sportstore.WebUI.Controllers;
using System.Collections.Generic;
using System.Linq;
using Moq;

namespace UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index()
        {
            //arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m=>m.Products).Returns(new Product[] 
            {
                new Product { Name = "P0", ProductId = 0, Price = 1 },
                new Product { Name = "P1", ProductId = 1, Price = 4 },
                new Product { Name = "P2", ProductId = 2, Price = 5 }
            }.AsQueryable());
            AdminController adminController = new AdminController(mock.Object);
            //act
            Product[] result = ((IEnumerable<Product>)adminController.Index().ViewData.Model).ToArray();
            //assert
            Assert.AreEqual("P1", result[0].Name);
            Assert.AreEqual("P2", result[1].Name);
            Assert.AreEqual("P3", result[2].Name);

        }
    }
}
