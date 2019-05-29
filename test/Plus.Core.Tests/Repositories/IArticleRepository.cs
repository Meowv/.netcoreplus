using MongoDB.Bson;
using Plus.Core.Tests.Domain;
using Plus.Domain.Repositories;

namespace Plus.Core.Tests.Repositories
{
    public interface IArticleRepository : IRepository<Article, ObjectId>
    {
    }
}