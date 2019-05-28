using Plus.Domain.Entities;
using Plus.EntityFramework;
using Plus.EntityFramework.Repositories;

namespace Plus.EFCore.Test
{
    public abstract class BlogRepositoryBase<TEntity, TPrimaryKey> : EfCoreRepositoryBase<BlogDbContext, TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        public BlogRepositoryBase(IDbContextProvider<BlogDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }

    public abstract class BlogRepositoryBase<TEntity> : EfCoreRepositoryBase<BlogDbContext, TEntity, int> where TEntity : class, IEntity<int>, new()
    {
        public BlogRepositoryBase(IDbContextProvider<BlogDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}