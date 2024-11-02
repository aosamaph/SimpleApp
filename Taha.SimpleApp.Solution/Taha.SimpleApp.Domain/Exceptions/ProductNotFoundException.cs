using Taha.SimpleApp.Domain.Entities;

namespace Taha.SimpleApp.Domain.Exceptions
{
    public class ProductNotFoundException(Product? product) : EntityException<Product>("Product not found", product)
    {
    }
}
