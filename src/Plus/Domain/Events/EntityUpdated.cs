namespace Plus.Domain.Events
{
    public class EntityUpdated<T>
    {
        public T Entity { get; private set; }

        public EntityUpdated(T entity)
        {
            Entity = entity;
        }
    }
}