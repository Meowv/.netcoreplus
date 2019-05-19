namespace Plus.MongoDb.Configuration
{
    /// <summary>
    /// IMongoDbModuleConfiguration
    /// </summary>
    public interface IMongoDbModuleConfiguration
    {
        string ConnectionString { get; set; }

        string DatabaseName { get; set; }
    }
}