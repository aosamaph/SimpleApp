using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Taha.SimpleApp.Application.Interfaces;
using Taha.SimpleApp.Application.Services.Categories;
using Taha.SimpleApp.Application.Services.Products;
using Taha.SimpleApp.Domain.Aggregates;
using Taha.SimpleApp.Domain.Exceptions;
using Xunit;

namespace Taha.SimpleApp.Application.xUnit.Services.Categories
{
    public class CategoryService_Test
    {
        private const string _categoryName = "Test Category";

        readonly Mock<IRepository<Category, int>> _categoryRepository;
        readonly Mock<IProductService> _productService;
        readonly Mock<ILogger<CategoryService>> _logger;

        readonly CategoryService _categoryService;

        public CategoryService_Test()
        {
            _logger = new();
            _productService = new();
            _categoryRepository = new();
            _categoryService = new CategoryService(_categoryRepository.Object, _productService.Object, _logger.Object);
        }

        [Fact]
        public void CreateCategoryWithTheSameName_ThrowDuplicateCategoryException()
        {
            Category category = new(_categoryName);
            _categoryRepository.Setup(r => r.Get())
               .Returns(new List<Category> { category }.AsQueryable());

            Assert.Throws<DuplicateCategoryException>(() => _categoryService.Create(_categoryName));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void CreateCategoryWithNullOrWhiteSpaceName_ThrowArgumentNullException(string categoryName)
        {
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => _categoryService.Create(categoryName));
            Assert.Equal("name", ex.ParamName);
        }

        [Fact]
        public void CreateCategory_ReturnsId()
        {
            int id = 1;
            _categoryRepository.Setup(r => r.Create(It.Is<Category>(c => c.Name == _categoryName)))
                .Returns(id);

            var categoryId = _categoryService.Create(_categoryName);

            Assert.Equal(id, categoryId);
            _categoryRepository.Verify(r => r.Create(It.Is<Category>(c => c.Name == _categoryName)), Times.Once);
        }

        [Fact]
        public void DeleteCategory_ReturnsTrue()
        {
            int categoryId = 1;
            _categoryRepository.Setup(r => r.Delete(categoryId)).Verifiable();

            bool result = _categoryService.Delete(categoryId, false);

            Assert.True(result);
            _categoryRepository.Verify(r => r.Delete(categoryId), Times.Once);
        }

        [Fact]
        public void DeleteCategoryWithProducts_ReturnsTrue()
        {
            int categoryId = 1;
            _categoryRepository.Setup(r => r.Delete(categoryId)).Verifiable();
            _productService.Setup(p => p.DeleteProducts(categoryId)).Verifiable();

            bool result = _categoryService.Delete(categoryId, true);

            Assert.True(result);
            _productService.Verify(p => p.DeleteProducts(categoryId), Times.Once);
            _categoryRepository.Verify(r => r.Delete(categoryId), Times.Once);
        }

        [Fact]
        public void DeleteCategoryWithProducts_LogsErrorAndReturnsFalse()
        {
            int categoryId = 1;
            _productService.Setup(r => r.DeleteProducts(categoryId)).Throws(new Exception("Test exception"));

            bool result = _categoryService.Delete(categoryId, true);

            Assert.False(result);
        }

        [Fact]
        public void GetCategory_ReturnsCategoryDto()
        {
            int categoryId = 1;
            Category category = new(_categoryName) { Id = categoryId };
            _categoryRepository.Setup(r => r.Get())
                .Returns(new List<Category> { category }.AsQueryable());

            var result = _categoryService.Get(categoryId);

            Assert.NotNull(result);
            Assert.Equal(categoryId, result.Id);
            Assert.Equal(_categoryName, result.Name);
        }

        [Fact]
        public void GetCategory_ThrowsCategoryNotFoundException()
        {
            int categoryId = 1;
            _categoryRepository.Setup(r => r.Get())
                .Returns(new List<Category>().AsQueryable());

            Assert.Throws<CategoryNotFoundException>(() => _categoryService.Get(categoryId));
        }

        [Fact]
        public void GetAllCategories_ReturnsCategoryDtos()
        {
            List<Category> categories =
            [
                new("Category 1"){Id = 1},
                new("Category 2"){Id = 2}
            ];
            _categoryRepository.Setup(r => r.Get())
                .Returns(categories.AsQueryable());

            var result = _categoryService.GetAll();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, c => c.Name == "Category 1");
            Assert.Contains(result, c => c.Name == "Category 2");
        }
    }
}
