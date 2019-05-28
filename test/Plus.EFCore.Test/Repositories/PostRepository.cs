using Plus.Core.Tests.Domain;
using Plus.Core.Tests.Repositories;
using Plus.EntityFramework;

namespace Plus.EFCore.Test.Repositories
{
    public class PostRepository : BlogRepositoryBase<Post>, IPostRepository
    {
        public PostRepository(IDbContextProvider<BlogDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}