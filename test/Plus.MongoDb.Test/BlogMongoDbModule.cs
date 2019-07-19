using Plus.Modules;
using Plus.MongoDb.Test.Mappers;
using System.Reflection;

namespace Plus.MongoDb.Test
{
    [DependsOn(typeof(PlusMongoDbModule))]
    public class BlogMongoDbModule : PlusModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<BlogMongoDbDatabaseProvider>();

            ArticleMapper.CreateMappings();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssembly(Assembly.GetExecutingAssembly());
        }
    }
}