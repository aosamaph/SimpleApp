using Taha.SimpleApp.Domain.Entities;

namespace Taha.SimpleApp.Infrastructure.Persistence.EF
{
    public class ProductRepository(AppDbContext context) : RepositoryBase<Product, int>(context)
    {
    }
}
