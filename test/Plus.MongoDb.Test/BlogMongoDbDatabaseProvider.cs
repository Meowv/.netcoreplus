using MongoDB.Driver;
using Plus.Core.Tests.Configuration;
using System.Collections.Generic;

namespace Plus.MongoDb.Test
{
    public class BlogMongoDbDatabaseProvider : IMongoDatabaseProvider
    {
        public IMongoClient Client { get; }
        public IMongoDatabase Database { get; }

        public BlogMongoDbDatabaseProvider()
        {
            var settings = new MongoClientSettings();
            var servers = new List<MongoServerAddress>();
            AppSettings.MongoDb.Servers.ForEach(x =>
            {
                servers.Add(new MongoServerAddress(x.Host, x.Port));
            });
            settings.Servers = servers;
            if (AppSettings.MongoDb.Username.IsNotNullOrEmpty())
            {
                settings.Credential = MongoCredential.CreateCredential("admin", AppSettings.MongoDb.Username, AppSettings.MongoDb.Password);
            }

            if (AppSettings.MongoDb.ConnectionMode.ToLower() == "replicaset")
            {
                settings.ConnectionMode = ConnectionMode.ReplicaSet;
                settings.ReadPreference = new ReadPreference(ReadPreferenceMode.SecondaryPreferred);
            }

            Client = new MongoClient(settings);

            Database = Client.GetDatabase(AppSettings.MongoDb.DatabaseName);
        }
    }
}