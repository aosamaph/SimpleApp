using Microsoft.Extensions.Logging;
using Taha.SimpleApp.Application.Interfaces;
using Taha.SimpleApp.Domain.Entities;
using Taha.SimpleApp.Domain.Exceptions;
using Taha.SimpleApp.Domain.ValueObjects;

namespace Taha.SimpleApp.Application.Services.Products
{
    internal class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IRepository<Product, int> _productRepository;

        public ProductService(ILogger<ProductService> logger, IRepository<Product, int> productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public bool ApplyDiscount(int productId, decimal discount)
        {
            if (discount >= 1 || discount <= 0)
                throw new ArgumentOutOfRangeException(nameof(discount), "Discount must be between 0 and 1");

            Product product = GetProductById(productId);
            product.Price.Multiply(1 - discount);
            return UpdateProduct(product);
        }

        public int CreateProduct(int categoryId, string productName, decimal price, Currency currency)
        {
            Product product = new(productName, "") { CategoryId = categoryId, Price = new(10, currency) };
            _productRepository.Create(product);
            _productRepository.SaveChanges();
            return product.Id;
        }

        public bool DeleteProduct(int productId)
        {
            Product? product = _productRepository.Delete(productId);
            if (product is null)
                throw new ProductNotFoundException(product);
            _productRepository.SaveChanges();
            return true;
        }

        public bool DeleteProducts(int categoryId)
        {
            try
            {
                var ids = GetProducts(categoryId).Select(p => p.Id);

                foreach (var id in ids)
                    _productRepository.Delete(id);

                _productRepository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while deleting products in category with id {CategoryId}", categoryId);
                return false;
            }
        }

        public ProductDto GetProduct(int productId)
        {
            Product product = GetProductById(productId);

            return ProductDto.From(product);
        }

        public IEnumerable<ProductDto> GetProducts(int categoryId)
        {
            return _productRepository.Get()
                .Where(p => p.CategoryId == categoryId)
                .Select(p => ProductDto.From(p))
                .ToList();
        }

        public bool UpdateDescription(int productId, string description)
        {
            Product product = GetProductById(productId);
            product.Description = description;
            return UpdateProduct(product);
        }

        public bool UpdateImage(int productId, string image)
        {
            Product product = GetProductById(productId);
            product.Image = image;
            return UpdateProduct(product);
        }

        public Product GetProductById(int productId)
        {
            var product = _productRepository.Get().Where(p => p.Id == productId).FirstOrDefault();
            if (product is null)
                throw new ProductNotFoundException(product);

            return product;
        }

        public bool UpdateProduct(Product product)
        {
            try
            {
                _productRepository.Update(product);
                _productRepository.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occured while updating the product with id {ProductId}", product.Id);
                return false;
            }
        }
    }
}
