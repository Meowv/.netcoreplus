namespace Plus.Domain.Entities
{
    /// <summary>
    /// IAggregateRoot
    /// </summary>
    public interface IAggregateRoot : IAggregateRoot<int>, IEntity<int>, IGeneratesDomainEvents, IEntity
    {
    }

    /// <summary>
    /// IAggregateRoot
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface IAggregateRoot<TPrimaryKey> : IEntity<TPrimaryKey>, IGeneratesDomainEvents
    {
    }
}