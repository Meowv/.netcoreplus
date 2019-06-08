using Plus.Modules;
using System.Reflection;

namespace Plus.MongoDb.Test
{
    [DependsOn(typeof(PlusMongoDbModule))]
    public class BlogMongoDbModule : PlusModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<BlogMongoDbDatabaseProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssembly(Assembly.GetExecutingAssembly());
        }
    }
}