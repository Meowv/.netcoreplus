namespace Plus.Domain.Events
{
    public class EntityInserted<T>
    {
        public T Entity { get; private set; }

        public EntityInserted(T entity)
        {
            Entity = entity;
        }
    }
}