using MongoDB.Driver;

namespace Plus.MongoDb
{
    /// <summary>
    /// IMongoDatabaseProvider
    /// </summary>
    public interface IMongoDatabaseProvider
    {
        IMongoClient Client { get; }

        IMongoDatabase Database { get; }
    }
}