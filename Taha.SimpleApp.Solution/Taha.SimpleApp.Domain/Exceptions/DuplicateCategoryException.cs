using Taha.SimpleApp.Domain.Aggregates;

namespace Taha.SimpleApp.Domain.Exceptions
{
    public class DuplicateCategoryException(Category category) : EntityException<Category>("Category already exists", category)
    {
    }
}
