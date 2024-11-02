using Taha.SimpleApp.Application.Services.Categories;
using Taha.SimpleApp.Application.Services.Products;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DI
    {
        public static IServiceCollection AddSimpleApp(this IServiceCollection services) =>
            services.AddScoped<ICategoryService, CategoryService>()
                .AddScoped<IProductService, ProductService>()
            ;
    }
}
