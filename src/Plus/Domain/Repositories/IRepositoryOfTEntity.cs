using Plus.Domain.Entities;

namespace Plus.Domain.Repositories
{
    /// <summary>
    /// <see cref="IRepository{TEntity,TPrimaryKey}"/>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> : IRepository<TEntity, int> where TEntity : class, IEntity<int>
    {

    }
}