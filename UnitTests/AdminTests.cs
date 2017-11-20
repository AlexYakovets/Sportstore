using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sportore.Domain.Abstract;
using Sportore.Domain.Entities;
using Sportstore.WebUI.Controllers;
using System.Collections.Generic;
using System.Linq;
using Moq;
using System.Web.Mvc;

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
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product { Name = "P0", ProductId = 0, Price = 1 },
                new Product { Name = "P1", ProductId = 1, Price = 4 },
                new Product { Name = "P2", ProductId = 2, Price = 5 }
            }.AsQueryable());
            AdminController adminController = new AdminController(mock.Object);
            //act
            Product[] result = ((IEnumerable<Product>)adminController.Index().ViewData.Model).ToArray();
            //assert
            Assert.AreEqual("P0", result[0].Name);
            Assert.AreEqual("P1", result[1].Name);
            Assert.AreEqual("P2", result[2].Name);

        }
        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            // Arrange - create mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object);
            // Arrange - create a product
            Product product = new Product { Name = "Test" };
            // Act - try to save the product
            ActionResult result = target.Edit(product);
            // Assert - check that the repository was called
            mock.Verify(m => m.SaveProduct(product));
            // Assert - check the method result type
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            // Arrange - create mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object);
            // Arrange - create a product
            Product product = new Product { Name = "Test" };
            // Arrange - add an error to the model state
            target.ModelState.AddModelError("error", "error");
            // Act - try to save the product
            ActionResult result = target.Edit(product);
            // Assert - check that the repository was not called
            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never());
            // Assert - check the method result type
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void Can_Delete_product()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            Product productForDelete = new Product() { ProductId = 5, Name = "DelPr" };
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product(){Name="p1",ProductId=0 },
                new Product(){Name="p2",ProductId=1 },
                productForDelete,
                new Product(){Name="p3",ProductId=2 }
            }.AsQueryable());
            AdminController currentController = new AdminController(mock.Object);
            //Action
            currentController.Delete(productForDelete.ProductId);
            //Assert
            //Assert.AreEqual(3, mock.Object.Products.Count());
            mock.Verify(m => m.DeleteProduct(productForDelete.ProductId));
        }
    }
}
