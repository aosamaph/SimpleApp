using Taha.SimpleApp.Domain.Aggregates;

namespace Taha.SimpleApp.Domain.Exceptions
{
    public class CategoryNotFoundException(Category? category) : EntityException<Category>("Category not found", category)
    {
    }
}
