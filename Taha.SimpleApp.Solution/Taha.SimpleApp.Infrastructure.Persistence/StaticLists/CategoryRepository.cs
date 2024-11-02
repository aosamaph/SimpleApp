using System.Collections.Concurrent;
using Taha.SimpleApp.Application.Interfaces;
using Taha.SimpleApp.Domain.Aggregates;

namespace Taha.SimpleApp.Infrastructure.Persistence.StaticLists
{
    public class CategoryRepository : IRepository<Category, int>
    {
        private readonly ConcurrentDictionary<int, Category> _categories = new();

        public int Create(Category entity)
        {
            entity.Id = !_categories.IsEmpty ? _categories.Keys.Max() + 1 : 1;
            _categories[entity.Id] = entity;
            return entity.Id;
        }

        public Category? Delete(int id)
        {
            _categories.TryRemove(id, out var category);
            return category;
        }

        public bool Exists(int id)
        {
            return _categories.ContainsKey(id);
        }

        public IQueryable<Category> Get()
        {
            return _categories.Values.AsQueryable();
        }

        public void SaveChanges()
        {
        }

        public void Update(Category entity)
        {
            _categories[entity.Id] = entity;
        }
    }
}