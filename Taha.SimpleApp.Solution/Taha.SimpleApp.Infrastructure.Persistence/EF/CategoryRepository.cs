using Taha.SimpleApp.Domain.Aggregates;

namespace Taha.SimpleApp.Infrastructure.Persistence.EF
{
    public class CategoryRepository(AppDbContext context) : RepositoryBase<Category, int>(context)
    {
    }

}
