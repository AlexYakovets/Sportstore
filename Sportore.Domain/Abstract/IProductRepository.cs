using System.Linq;
using Sportore.Domain.Entities;

namespace Sportore.Domain.Abstract
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        void SaveProduct(Product product);
    }
}
