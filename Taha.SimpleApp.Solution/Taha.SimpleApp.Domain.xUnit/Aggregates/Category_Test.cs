using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taha.SimpleApp.Domain.Aggregates;
using Taha.SimpleApp.Domain.Entities;
using Taha.SimpleApp.Domain.Exceptions;
using Taha.SimpleApp.Domain.ValueObjects;

namespace Taha.SimpleApp.Domain.xUnit.Aggregates
{
    public class Category_Test
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void CreateCategoryWithNullOrWhiteSpaceName_ThrowArgumentNullException(string name)
        {
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => new Category(name));
            Assert.Equal("name", ex.ParamName);
        }

        [Fact]
        public void CreateCategoryWithName_Success()
        {
            Category category = CreateTestCategory();
            Assert.Equal("Test Category", category.Name);
            Assert.Empty(category.Products);
        }

        [Fact]
        public void AddNullProductToCategory_ThrowArgumentNullException()
        {
            Category category = CreateTestCategory();
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => category.AddProduct(null));
            Assert.Equal("product", ex.ParamName);
        }

        [Fact]
        public void AddProductToCategory_Success()
        {
            Product product = CreateTestProduct();
            Category category = CreateTestCategory();

            category.AddProduct(product);

            var p = Assert.Single(category.Products);
            Assert.Same(product, p);
            Assert.Equal(product, p);
        }

        [Fact]
        public void RemoveProductFromCategory_Success()
        {
            Product product = CreateTestProduct();
            Category category = CreateTestCategory();
            category.AddProduct(product);

            category.RemoveProduct(product);

            Assert.Empty(category.Products);
        }

        [Fact]
        public void RemoveProductFromCategory_ThrowArgumentNullException()
        {
            Category category = CreateTestCategory();

            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => category.RemoveProduct(null));
            Assert.Equal("product", ex.ParamName);
        }

        [Fact]
        public void RemoveProductFromCategory_ThrowProductNotFoundException()
        {
            Category category = CreateTestCategory();
            Product product = CreateTestProduct();

            ProductNotFoundException ex = Assert.Throws<ProductNotFoundException>(() => category.RemoveProduct(product));

            Assert.Equal(product, ex.Entity);
            Assert.Same(product, ex.Entity);
            Assert.Equal("Product not found", ex.Message);
        }

        [Fact]
        public void AddDuplicateProduct_ThrowDuplicateProductException()
        {
            Category category = CreateTestCategory();
            Product product = CreateTestProduct();
            category.AddProduct(product);

            DuplicateProductException ex = Assert.Throws<DuplicateProductException>(() => category.AddProduct(product));

            Assert.Equal(product, ex.Entity);
            Assert.Same(product, ex.Entity);
            Assert.Equal("Product already exists", ex.Message);
        }


        private static Category CreateTestCategory() => new("Test Category");
        private static Product CreateTestProduct() => new("Test Product", "Test Description") { Price = new MoneyAmount(1, Currency.USD) };
    }
}
