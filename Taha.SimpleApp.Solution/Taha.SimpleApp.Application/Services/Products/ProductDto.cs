using Taha.SimpleApp.Domain.Entities;

namespace Taha.SimpleApp.Application.Services.Products
{
    public record ProductDto(int Id, int CategoryId, string Name, decimal Price, string Currency, string Image, string Description)
    {
        public static ProductDto From(Product p) => new(p.Id, p.CategoryId, p.Name, p.Price.Price, p.Price.Currency.ToString(), p.Image, p.Description);
    }
}