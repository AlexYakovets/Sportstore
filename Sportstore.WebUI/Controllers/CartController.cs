using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sportore.Domain.Abstract;
using Sportore.Domain.Entities;
using Sportstore.WebUI.Models;

namespace Sportstore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private IOrderProcessor orderProcessor;

        public CartController(IProductRepository repo,IOrderProcessor proc)
        {
            repository = repo;
            orderProcessor = proc;
        }
        public RedirectToRouteResult AddToCart(Cart cart,int productid, string returnurl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductId == productid);
            if (product!= null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnurl });
        }
        public RedirectToRouteResult RemoveFromCart(Cart cart,int productId, string returnurl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnurl });
        }
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
        public ViewResult Index(Cart cart, string returnurl)
        {
            return View(new CartIndexViewModel{
                Cart = cart,
                ReturnUrl = returnurl});
        }
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }
        [HttpPost]
        public ViewResult Checkout(Cart cart,ShippingDetails shippingDetails){
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("","Sorry your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                orderProcessor.ProcessorOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            return View(new ShippingDetails());
        }
     
    }
}