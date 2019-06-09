using MongoDB.Driver;

namespace Plus.MongoDb
{
    public interface IMongoDatabaseProvider
    {

        IMongoClient Client { get; }

        IMongoDatabase Database { get; }
    }
}