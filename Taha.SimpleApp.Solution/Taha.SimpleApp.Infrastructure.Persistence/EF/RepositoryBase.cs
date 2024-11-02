using Microsoft.EntityFrameworkCore;
using Taha.SimpleApp.Application.Interfaces;

namespace Taha.SimpleApp.Infrastructure.Persistence.EF
{
    public abstract class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class
    where TKey : struct, IEquatable<TKey>
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        private protected RepositoryBase(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public bool Exists(TKey id)
        {
            return _dbSet.Find(id) != null;
        }

        public TKey Create(TEntity entity)
        {
            _dbSet.Add(entity);            
            var keyProperty = _context.Entry(entity).Property("Id").CurrentValue;
            return (TKey)keyProperty;
        }

        public TEntity? Delete(TKey id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
            return entity;
        }

        public IQueryable<TEntity> Get()
        {
            return _dbSet.AsQueryable();
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }


}
