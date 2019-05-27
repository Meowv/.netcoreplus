namespace Plus.Domain.Events
{
    public class EntityDeleted<T>
    {
        public T Entity { get; private set; }

        public EntityDeleted(T entity)
        {
            Entity = entity;
        }
    }
}