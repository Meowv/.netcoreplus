using MongoDB.Bson;
using MongoDB.Driver;
using Plus.Domain.Entities;
using Plus.MongoDb.Repositories;
using System.Linq;

namespace Plus.MongoDb.Test
{
    public abstract class BlogMongoDbRepositoryBase
    {
        private readonly BlogMongoDbDatabaseProvider _blogMongoDbDatabaseProvider;

        public BlogMongoDbRepositoryBase(BlogMongoDbDatabaseProvider blogMongoDbDatabaseProvider)
        {
            _blogMongoDbDatabaseProvider = blogMongoDbDatabaseProvider;
        }

        public IMongoClient Client => _blogMongoDbDatabaseProvider.Client;

        public IMongoDatabase Database => _blogMongoDbDatabaseProvider.Database;
    }

    public abstract class BlogMongoDbTakeAutoIncRepositoryBase<TEntity> : BlogMongoDbRepositoryBase<TEntity, ObjectId> where TEntity : class, IEntity<ObjectId>
    {
        public BlogMongoDbTakeAutoIncRepositoryBase(BlogMongoDbDatabaseProvider databaseProvider) : base(databaseProvider)
        {

        }

        public override IQueryable<TEntity> GetAll()
        {
            return base.GetAll();
        }
    }

    public abstract class BlogMongoDbRepositoryBase<TEntity> : BlogMongoDbRepositoryBase<TEntity, ObjectId>
            where TEntity : class, IEntity<ObjectId>
    {
        public BlogMongoDbRepositoryBase(BlogMongoDbDatabaseProvider databaseProvider) : base(databaseProvider) { }
    }

    public abstract class BlogMongoDbRepositoryBase<TEntity, TPrimaryKey> : MongoDbRepositoryBase<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        public BlogMongoDbRepositoryBase(BlogMongoDbDatabaseProvider databaseProvider) : base(databaseProvider) { }
    }
}