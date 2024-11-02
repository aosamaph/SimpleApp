using Taha.SimpleApp.Domain.Exceptions;

namespace Taha.SimpleApp.Application.Services.Categories
{
    public interface ICategoryService
    {
        /// <exception cref="DuplicateCategoryException"/>
        /// <exception cref="ArgumentNullException"/>
        int Create(string name);

        /// <exception cref="CategoryNotFoundException"/>
        bool Delete(int categoryId, bool deleteProducts);

        /// <exception cref="CategoryNotFoundException"/>
        CategoryDto Get(int categoryId);

        IEnumerable<CategoryDto> GetAll();
    }
}