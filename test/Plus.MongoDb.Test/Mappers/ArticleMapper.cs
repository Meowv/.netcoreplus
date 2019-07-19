using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using Plus.Core.Tests.Domain;

namespace Plus.MongoDb.Test.Mappers
{
    internal static class ArticleMapper
    {
        private static volatile bool _mappedBefore;
        private static readonly object _syncObj = new object();

        public static void CreateMappings()
        {
            lock (_syncObj)
            {
                if (_mappedBefore)
                    return;

                CreateMappingsInternal();

                _mappedBefore = true;
            }
        }

        private static void CreateMappingsInternal()
        {
            BsonClassMap.RegisterClassMap<Article>(u =>
            {
                u.AutoMap();
                u.MapIdMember(x => x.Id).SetIdGenerator(ObjectIdGenerator.Instance);
                u.SetIgnoreExtraElements(true);
            });
        }
    }
}