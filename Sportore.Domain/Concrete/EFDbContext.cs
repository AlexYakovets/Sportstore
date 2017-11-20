using Sportore.Domain.Entities;
using System.Data.Entity;

namespace Sportore.Domain.Concrete
{
    class EFDbContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}
