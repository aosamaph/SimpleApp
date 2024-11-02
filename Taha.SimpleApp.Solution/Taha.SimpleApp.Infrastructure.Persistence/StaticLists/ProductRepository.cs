using System.Collections.Concurrent;
using Taha.SimpleApp.Application.Interfaces;
using Taha.SimpleApp.Domain.Entities;

namespace Taha.SimpleApp.Infrastructure.Persistence.StaticLists
{
    public class ProductRepository : IRepository<Product, int>
    {
        private readonly ConcurrentDictionary<int, Product> _products = new();

        public int Create(Product entity)
        {
            entity.Id = !_products.IsEmpty ? _products.Keys.Max() + 1 : 1;
            _products[entity.Id] = entity;
            return entity.Id;
        }

        public Product? Delete(int id)
        {
            _products.TryRemove(id, out var product);
            return product;
        }

        public bool Exists(int id)
        {
            return _products.ContainsKey(id);
        }

        public IQueryable<Product> Get()
        {
            return _products.Values.AsQueryable();
        }

        public void SaveChanges()
        {

        }

        public void Update(Product entity)
        {
            _products[entity.Id] = entity;
        }
    }

}
