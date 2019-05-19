namespace Plus.MongoDb.Configuration
{
    /// <summary>
    /// MongoDbModuleConfiguration
    /// </summary>
    internal class MongoDbModuleConfiguration : IMongoDbModuleConfiguration
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}