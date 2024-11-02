using Taha.SimpleApp.Domain.Exceptions;
using Taha.SimpleApp.Domain.ValueObjects;

namespace Taha.SimpleApp.Application.Services.Products
{
    public interface IProductService
    {
        /// <exception cref="CategoryNotFoundException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        int CreateProduct(int categoryId, string productName, decimal price, Currency currency);

        /// <exception cref="ProductNotFoundException"></exception>
        ProductDto GetProduct(int productId);

        IEnumerable<ProductDto> GetProducts(int categoryId);

        /// <exception cref="ProductNotFoundException"></exception>
        bool UpdateImage(int productId, string image);

        /// <exception cref="ProductNotFoundException"></exception>
        /// <exception cref="ArgumentException">Thrown when description length exceeds the max length</exception>
        bool UpdateDescription(int productId, string description);

        /// <exception cref="ProductNotFoundException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        bool ApplyDiscount(int productId, decimal discount);

        /// <exception cref="ProductNotFoundException"></exception>
        bool DeleteProduct(int productId);

        /// <exception cref="CategoryNotFoundException"></exception>
        bool DeleteProducts(int categoryId);
    }
}
