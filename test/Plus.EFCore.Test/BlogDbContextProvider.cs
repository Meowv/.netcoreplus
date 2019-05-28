using Plus.Dependency;
using Plus.Domain.Uow;
using Plus.EntityFramework;
using Plus.EntityFramework.Uow;

namespace Plus.EFCore.Test
{
    public class BlogDbContextProvider : IDbContextProvider<BlogDbContext>, ITransientDependency
    {
        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWorkProvider;

        public BlogDbContextProvider(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider)
        {
            _currentUnitOfWorkProvider = currentUnitOfWorkProvider;
        }

        public BlogDbContext GetDbContext()
        {
            return ((EfCoreUnitOfWork)_currentUnitOfWorkProvider.Current).GetDbContext<BlogDbContext>();
        }
    }
}