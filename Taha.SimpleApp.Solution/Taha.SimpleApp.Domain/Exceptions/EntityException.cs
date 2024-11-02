namespace Taha.SimpleApp.Domain.Exceptions
{
    public abstract class EntityException<T>(string message, T? entity) : Exception(message) where T : class
    {
        public T? Entity { get; } = entity;
    }
}