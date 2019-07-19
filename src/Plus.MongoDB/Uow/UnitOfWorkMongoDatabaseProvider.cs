using MongoDB.Driver;
using Plus.MongoDb.Configuration;

namespace Plus.MongoDb.Uow
{
    /// <summary>
    /// Implements <see cref="IMongoDatabaseProvider"/> that gets database from active unit of work.
    /// </summary>
    public class UnitOfWorkMongoDatabaseProvider : IMongoDatabaseProvider
    {
        private IMongoDbModuleConfiguration _configuration;

        public IMongoClient Client { get; private set; }

        public IMongoDatabase Database { get; private set; }

        public UnitOfWorkMongoDatabaseProvider(IMongoDbModuleConfiguration configuration)
        {
            _configuration = configuration;
            Client = new MongoClient(_configuration.ConnectionString);
            Database = Client.GetDatabase(_configuration.DatabaseName, null);
        }
    }
}