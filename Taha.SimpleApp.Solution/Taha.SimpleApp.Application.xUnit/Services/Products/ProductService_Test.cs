using Microsoft.Extensions.Logging;
using Moq;
using Taha.SimpleApp.Application.Interfaces;
using Taha.SimpleApp.Application.Services.Products;
using Taha.SimpleApp.Domain.Entities;
using Taha.SimpleApp.Domain.Exceptions;
using Taha.SimpleApp.Domain.ValueObjects;

namespace Taha.SimpleApp.Application.xUnit.Services.Products
{
    public class ProductService_Test
    {
        private readonly Mock<ILogger<ProductService>> _logger;
        private readonly Mock<IRepository<Product, int>> _productRepository;
        private readonly ProductService _productService;

        public ProductService_Test()
        {
            _logger = new Mock<ILogger<ProductService>>();
            _productRepository = new Mock<IRepository<Product, int>>();
            _productService = new ProductService(_logger.Object, _productRepository.Object);
        }

        [Fact]
        public void ApplyDiscount_UpdatesProductPrice()
        {
            int productId = 1;
            decimal discount = 0.1m;
            var product = new Product("Test Product", new MoneyAmount(100, Currency.USD), "") { Id = productId };
            _productRepository.Setup(r => r.Get()).Returns(new List<Product> { product }.AsQueryable());

            bool result = _productService.ApplyDiscount(productId, discount);

            Assert.True(result);
            Assert.Equal(90, product.Price.Price);
            _productRepository.Verify(r => r.Update(product), Times.Once);
        }

        [Fact]
        public void CreateProduct_ReturnsProductId()
        {
            int categoryId = 1;
            string productName = "Test Product";
            decimal price = 100;
            int productId = 1;
            _productRepository.Setup(r => r.Create(It.IsAny<Product>())).Returns(productId);

            int result = _productService.CreateProduct(categoryId, productName, price);

            Assert.Equal(productId, result);
            _productRepository.Verify(r => r.Create(It.Is<Product>(p => p.Name == productName && p.CategoryId == categoryId)), Times.Once);
        }

        [Fact]
        public void DeleteProduct_ThrowsProductNotFoundException()
        {
            int productId = 1;
            _productRepository.Setup(r => r.Delete(productId)).Returns((Product)null);

            Assert.Throws<ProductNotFoundException>(() => _productService.DeleteProduct(productId));
        }

        [Fact]
        public void DeleteProduct_ReturnsTrue()
        {
            int productId = 1;
            var product = new Product("Test Product", new MoneyAmount(100, Currency.USD), "") { Id = productId };
            _productRepository.Setup(r => r.Delete(productId)).Returns(product);

            bool result = _productService.DeleteProduct(productId);

            Assert.True(result);
            _productRepository.Verify(r => r.Delete(productId), Times.Once);
        }

        [Fact]
        public void DeleteProducts_ReturnsTrue()
        {
            int categoryId = 1;
            var products = new List<Product>
            {
                new("Product 1", new MoneyAmount(100, Currency.USD), "") { Id = 1, CategoryId = categoryId },
                new("Product 2", new MoneyAmount(200, Currency.USD), "") { Id = 2, CategoryId = categoryId }
            };
            _productRepository.Setup(r => r.Get()).Returns(products.AsQueryable());

            bool result = _productService.DeleteProducts(categoryId);

            Assert.True(result);
            _productRepository.Verify(r => r.Delete(It.IsAny<int>()), Times.Exactly(products.Count));
        }

        [Fact]
        public void DeleteProducts_LogsErrorAndReturnsFalse()
        {
            int categoryId = 1;
            _productRepository.Setup(r => r.Get()).Throws(new Exception("Test exception"));

            bool result = _productService.DeleteProducts(categoryId);

            Assert.False(result);
        }

        [Fact]
        public void GetProduct_ReturnsProductDto()
        {
            int productId = 1;
            var product = new Product("Test Product", new MoneyAmount(100, Currency.USD), "") { Id = productId };
            _productRepository.Setup(r => r.Get()).Returns(new List<Product> { product }.AsQueryable());

            var result = _productService.GetProduct(productId);

            Assert.NotNull(result);
            Assert.Equal(productId, result.Id);
            Assert.Equal(product.Name, result.Name);
        }

        [Fact]
        public void GetProduct_ThrowsProductNotFoundException()
        {
            int productId = 1;
            _productRepository.Setup(r => r.Get()).Returns(new List<Product>().AsQueryable());

            Assert.Throws<ProductNotFoundException>(() => _productService.GetProduct(productId));
        }

        [Fact]
        public void GetProducts_ReturnsProductDtos()
        {
            int categoryId = 1;
            var products = new List<Product>
            {
                new("Product 1", new MoneyAmount(100, Currency.USD), "") { Id = 1, CategoryId = categoryId },
                new("Product 2", new MoneyAmount(200, Currency.USD), "") { Id = 2, CategoryId = categoryId }
            };
            _productRepository.Setup(r => r.Get()).Returns(products.AsQueryable());

            var result = _productService.GetProducts(categoryId);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, p => p.Name == "Product 1");
            Assert.Contains(result, p => p.Name == "Product 2");
        }

        [Fact]
        public void UpdateDescription_UpdatesProductDescription()
        {
            int productId = 1;
            string description = "New Description";
            var product = new Product("Test Product", new MoneyAmount(100, Currency.USD), "") { Id = productId };
            _productRepository.Setup(r => r.Get()).Returns(new List<Product> { product }.AsQueryable());

            bool result = _productService.UpdateDescription(productId, description);

            Assert.True(result);
            Assert.Equal(description, product.Description);
            _productRepository.Verify(r => r.Update(product), Times.Once);
        }

        [Fact]
        public void UpdateImage_UpdatesProductImage()
        {
            int productId = 1;
            string image = "New Image";
            var product = new Product("Test Product", new MoneyAmount(100, Currency.USD), "") { Id = productId };
            _productRepository.Setup(r => r.Get()).Returns(new List<Product> { product }.AsQueryable());

            bool result = _productService.UpdateImage(productId, image);

            Assert.True(result);
            Assert.Equal(image, product.Image);
            _productRepository.Verify(r => r.Update(product), Times.Once);
        }
    }
}
