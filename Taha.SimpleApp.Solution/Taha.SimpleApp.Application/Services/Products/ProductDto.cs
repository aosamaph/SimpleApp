using Taha.SimpleApp.Domain.Entities;
using Taha.SimpleApp.Domain.ValueObjects;

namespace Taha.SimpleApp.Application.Services.Products
{
    public record ProductDto(int Id, int CategoryId, string Name, decimal Price, Currency Currency, string Image, string Description)
    {
        public static ProductDto From(Product p) => new(p.Id, p.CategoryId, p.Name, p.Price.Price, p.Price.Currency, p.Image, p.Description);
    }
}