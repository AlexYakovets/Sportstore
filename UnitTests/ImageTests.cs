using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sportore.Domain.Entities;
using Sportore.Domain.Abstract;
using Moq;
using Sportstore.WebUI.Controllers;
using System.Web.Mvc;

namespace UnitTests
{
    [TestClass]
    public class ImageTests
    {
        [TestMethod]
        public void Can_Retrieve_Image_Data()
        {
            // Arrange - create the mock repository
            Product p = new Product
            {
                ProductId = 1,
                Name = "Product2",
                ImageData = new byte[0],
                ImageMimeType = "image/jpg"
            };
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
                {new Product{ProductId=0,Name="Product1" },
                p,
                new Product{ProductId=2,Name="Product3" }
                }.AsQueryable());
            // Arrange - create the controller
            ProductController controller = new ProductController(mock.Object);
            // Act - call the GetImage action method
            var result = controller.GetImage(1);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("image/jpg", result.ContentType);
            Assert.IsInstanceOfType(result, typeof(FileResult));
        }
        [TestMethod]
        public void Cannot_Retrieve_Image_Data_For_Invalid_ID()
        {
            // Arrange - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
            new Product {ProductId = 1, Name = "P1"},
            new Product {ProductId = 2, Name = "P2"}
            }.AsQueryable());
            // Arrange - create the controller
            ProductController target = new ProductController(mock.Object);
            // Act - call the GetImage action method
            ActionResult result = target.GetImage(100);
            // Assert
            Assert.IsNull(result);
        }
    }
}
