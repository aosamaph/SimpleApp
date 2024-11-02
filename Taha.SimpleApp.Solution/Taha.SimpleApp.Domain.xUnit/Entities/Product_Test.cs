using Taha.SimpleApp.Domain.Entities;
using Taha.SimpleApp.Domain.ValueObjects;

namespace Taha.SimpleApp.Domain.xUnit.Entities
{
    public class Product_Test
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void CreateProductWithNullOrWhitespace_ThrowArgumentNullException(string name)
        {
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => new Product(name, new(1, Currency.USD), ""));
            Assert.Equal(nameof(name), ex.ParamName);
        }

        [Fact]
        public void CreateProductWithNullPrice_ThrowArgumentNullException()
        {
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => new Product("Test Product", null, ""));
            Assert.Equal("price", ex.ParamName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void CreateProductWithNullOrWhitespace_DescriptionIsSetWithEmptyString(string description)
        {
            Product product = new("Test Product", new(1, Currency.USD), description);

            Assert.Equal(string.Empty, product.Description);
        }

        [Fact]
        public void CreateProductWithLongDescriptionExceedMaxLength_ThrowArgumentException()
        {
            string longDescription = GetStringWithLength(Product.DESCRIPTION_MAX_LENGTH + 1);

            ArgumentException ex = Assert.Throws<ArgumentException>(() => new Product("Test Product", new(1, Currency.USD), longDescription));
            Assert.Equal("description", ex.ParamName);
        }

        [Fact]
        public void SetProductDescriptionExceedMaxLength_ThrowArgumentException()
        {
            string shortDescription = GetStringWithLength(1);
            string longDescription = GetStringWithLength(Product.DESCRIPTION_MAX_LENGTH + 1);

            Product product = new("Test Product", new(1, Currency.USD), shortDescription);

            Assert.Throws<ArgumentException>(() => product.Description = longDescription);
            Assert.Equal(shortDescription, product.Description);
        }

        private static string GetStringWithLength(int length) => new('a', length);

    }
}