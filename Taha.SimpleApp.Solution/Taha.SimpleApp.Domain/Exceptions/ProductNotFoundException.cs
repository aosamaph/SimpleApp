using Taha.SimpleApp.Domain.Entities;

namespace Taha.SimpleApp.Domain.Exceptions
{
    public class ProductNotFoundException(Product product) : ProductException(product, "Product not found")
    {
    }
}
