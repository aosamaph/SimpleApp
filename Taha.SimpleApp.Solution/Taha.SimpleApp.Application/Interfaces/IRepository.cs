namespace Taha.SimpleApp.Application.Interfaces
{
    public interface IRepository<T, Tid>
        where T : class
        where Tid : struct, IEquatable<Tid>
    {
        bool Exists(Tid id);
        Tid Create(T entity);
        T? Delete(Tid id);
        IQueryable<T> Get();
        void Update(T entity);
        void SaveChanges();
    }
}
