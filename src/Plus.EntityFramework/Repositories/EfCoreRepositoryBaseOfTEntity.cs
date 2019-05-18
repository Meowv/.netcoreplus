using Microsoft.EntityFrameworkCore;
using Plus.Domain.Entities;
using Plus.Domain.Repositories;

namespace Plus.EntityFramework.Repositories
{
    /// <summary>
    /// EfCoreRepositoryBase
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public class EfCoreRepositoryBase<TDbContext, TEntity> : EfCoreRepositoryBase<TDbContext, TEntity, int>, IRepository<TEntity>
        where TEntity : class, IEntity<int>
        where TDbContext : DbContext
    {
        public EfCoreRepositoryBase(IDbContextProvider<TDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }
    }
}