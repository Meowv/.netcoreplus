using Plus.Core.Tests.Domain;
using Plus.Core.Tests.Repositories;

namespace Plus.MongoDb.Test.Repositories
{
    public class ArticleRepository : BlogMongoDbTakeAutoIncRepositoryBase<Article>, IArticleRepository
    {
        public ArticleRepository(BlogMongoDbDatabaseProvider databaseProvider) : base(databaseProvider)
        {
        }
    }
}