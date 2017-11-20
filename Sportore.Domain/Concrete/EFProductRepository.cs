using Sportore.Domain.Abstract;
using Sportore.Domain.Entities;
using System.Linq;


namespace Sportore.Domain.Concrete
{
    public class EFProductRepository:IProductRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Product> Products
        {
            get { return context.Products; }
        }
    }
}
