using Microsoft.Extensions.Logging;
using Taha.SimpleApp.Application.Interfaces;
using Taha.SimpleApp.Application.Services.Products;
using Taha.SimpleApp.Domain.Aggregates;
using Taha.SimpleApp.Domain.Exceptions;

namespace Taha.SimpleApp.Application.Services.Categories
{
    internal class CategoryService : ICategoryService
    {
        private readonly IRepository<Category, int> _categoryRepository;
        private readonly IProductService _productService;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(IRepository<Category, int> categoryRepository, IProductService productService, ILogger<CategoryService> logger)
        {
            _categoryRepository = categoryRepository;
            _productService = productService;
            _logger = logger;
        }

        public int Create(string name)
        {
            Category? category = _categoryRepository.Get().Where(c => c.Name == name).FirstOrDefault();
            if (category is not null)
                throw new DuplicateCategoryException(category);

            category = new(name);
            int id = _categoryRepository.Create(category);

            return id;
        }

        public bool Delete(int categoryId, bool deleteProducts)
        {
            try
            {
                if (deleteProducts)
                    _productService.DeleteProducts(categoryId);

                var deletedCategory = _categoryRepository.Delete(categoryId);
                if (deletedCategory is null)
                    return false;
                
                _categoryRepository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while deleting category {CategoryId}", categoryId);
                return false;
            }
        }

        public CategoryDto Get(int categoryId)
        {
            Category? category = _categoryRepository.Get()
                .Where(c => c.Id == categoryId)
                .FirstOrDefault();

            if (category is null)
                throw new CategoryNotFoundException(category);

            return new CategoryDto(category.Id, category.Name);
        }

        public IEnumerable<CategoryDto> GetAll()
        {
            return _categoryRepository.Get()
                .Select(c => new CategoryDto(c.Id, c.Name))
                .ToList();
        }
    }
}
