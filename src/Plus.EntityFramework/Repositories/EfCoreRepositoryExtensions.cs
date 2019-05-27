using Microsoft.EntityFrameworkCore;
using Plus.Domain.Entities;
using Plus.Domain.Repositories;
using Plus.Reflection;
using System;

namespace Plus.EntityFramework.Repositories
{
    /// <summary>
    /// EfCoreRepositoryExtensions
    /// </summary>
    public static class EfCoreRepositoryExtensions
    {
        public static DbContext GetDbContext<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository)
            where TEntity : class, IEntity<TPrimaryKey>
        {
            if (ProxyHelper.UnProxy(repository) is IRepositoryWithDbContext repositoryWithDbContext)
            {
                return repositoryWithDbContext.GetDbContext();
            }

            throw new ArgumentException("Given repository does not implement IRepositoryWithDbContext", nameof(repository));
        }

        public static void DetachFromDbContext<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository, TEntity entity)
            where TEntity : class, IEntity<TPrimaryKey>
        {
            repository.GetDbContext().Entry(entity).State = EntityState.Detached;
        }
    }
}