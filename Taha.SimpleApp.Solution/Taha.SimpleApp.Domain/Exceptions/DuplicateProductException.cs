using Taha.SimpleApp.Domain.Entities;

namespace Taha.SimpleApp.Domain.Exceptions
{
    public class DuplicateProductException(Product product) : ProductException(product, "Product already exists")
    {
    }
}
