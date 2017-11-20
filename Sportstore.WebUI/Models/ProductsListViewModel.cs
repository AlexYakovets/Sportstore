using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sportore.Domain.Entities;
namespace Sportstore.WebUI.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}