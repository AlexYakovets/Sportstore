using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Sportore.Domain.Entities;
using Sportore.Domain.Abstract;
using Sportore.Domain.Concrete;
using System.Collections.Generic;
using System.Linq;
using Moq;
using System.Configuration;
using Sportstore.WebUI.Infrastructure.Concrete;
using Sportstore.WebUI.Infrastructure.Abstract;


namespace Sportstore.WebUI.Infrastructure
{
    public class NinjectControllerFactory:DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }
        protected override IController GetControllerInstance(RequestContext requestContext,Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }
        private void AddBindings()
        {
            //Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //mock.Setup(m => m.Products).Returns(new List<Product>
            //    { new Product{Name="Football",Price=25},
            //      new Product{Name="Surf board",Price=179,Description="Board for surfing"},
            //      new Product{Name="Running shoes",Price=95 }
            //    }.AsQueryable());
            //ninjectKernel.Bind<IProductRepository>().ToConstant(mock.Object);
            ninjectKernel.Bind<IProductRepository>().To<EFProductRepository>();
            EmailSettings emailSettings = new EmailSettings()
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };
            ninjectKernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("settings", emailSettings);
            ninjectKernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
        }

        }

    }