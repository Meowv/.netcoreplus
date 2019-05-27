using Plus.Core.Tests.Domain;
using Plus.Domain.Repositories;

namespace Plus.Core.Tests.Repositories
{
    public interface IPostRepository : IRepository<Post, int>
    {
    }
}