using Microsoft.EntityFrameworkCore;
using Plus.Dependency;
using Plus.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plus.EntityFramework.Linq
{
    public class EfCoreAsyncQueryableExecuter : IAsyncQueryableExecuter, ISingletonDependency
    {
        public Task<int> CountAsync<T>(IQueryable<T> queryable)
        {
            return EntityFrameworkQueryableExtensions.CountAsync<T>(queryable, default);
        }

        public Task<List<T>> ToListAsync<T>(IQueryable<T> queryable)
        {
            return EntityFrameworkQueryableExtensions.ToListAsync<T>(queryable, default);
        }

        public Task<T> FirstOrDefaultAsync<T>(IQueryable<T> queryable)
        {
            return EntityFrameworkQueryableExtensions.FirstOrDefaultAsync<T>(queryable, default);
        }
    }
}