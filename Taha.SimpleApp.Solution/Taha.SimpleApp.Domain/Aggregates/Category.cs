using System.Collections.Concurrent;
using Taha.SimpleApp.Domain.Entities;
using Taha.SimpleApp.Domain.Exceptions;

namespace Taha.SimpleApp.Domain.Aggregates
{
    public class Category
    {
        public Category(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            Name = name;

            _products = [];
        }

        public string Name { get; }
        public int Id { get; set; }
        public IEnumerable<Product> Products { get => _products.Values; }

        private readonly ConcurrentDictionary<string, Product> _products;

        public void AddProduct(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);

            bool added = _products.TryAdd(product.Name, product);
            if (!added)
                throw new DuplicateProductException(product);
        }

        public void RemoveProduct(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);
            if (!_products.ContainsKey(product.Name))
                throw new ProductNotFoundException(product);

            _products.TryRemove(product.Name, out _);
        }
    }
}
