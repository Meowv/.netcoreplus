using Microsoft.EntityFrameworkCore;
using Plus.Dependency;
using Plus.Domain.Uow;

namespace Plus.EntityFramework.Uow
{
    /// <summary>
    /// IEfCoreTransactionStrategy
    /// </summary>
    public interface IEfCoreTransactionStrategy
    {
        void InitOptions(UnitOfWorkOptions options);

        DbContext CreateDbContext<TDbContext>(string connectionString, IDbContextResolver dbContextResolver) where TDbContext : DbContext;

        void Commit();

        void Dispose(IIocResolver iocResolver);
    }
}