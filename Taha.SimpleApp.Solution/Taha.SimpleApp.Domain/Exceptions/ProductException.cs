using Taha.SimpleApp.Domain.Entities;

namespace Taha.SimpleApp.Domain.Exceptions
{
    public abstract class ProductException(Product product, string message) : Exception(message)
    {
        public Product? Product { get; } = product;
    }
}