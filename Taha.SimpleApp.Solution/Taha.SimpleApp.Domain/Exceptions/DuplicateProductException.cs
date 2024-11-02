using Taha.SimpleApp.Domain.Entities;

namespace Taha.SimpleApp.Domain.Exceptions
{
    public class DuplicateProductException(Product product) : EntityException<Product>("Product already exists", product)
    {
    }
}
