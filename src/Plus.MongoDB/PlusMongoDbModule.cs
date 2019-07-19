using Plus.Modules;
using Plus.MongoDb.Configuration;
using Plus.MongoDb.Uow;
using System.Reflection;

namespace Plus.MongoDb
{
    /// <summary>
    /// MongoDB 数据访问层
    /// </summary>
    [DependsOn(typeof(PlusLeadershipModule))]
    public class PlusMongoDbModule : PlusModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IMongoDbModuleConfiguration, MongoDbModuleConfiguration>();
        }

        public override void Initialize()
        {
            IocManager.Register<IMongoDatabaseProvider, UnitOfWorkMongoDatabaseProvider>();
            IocManager.RegisterAssembly(Assembly.GetExecutingAssembly());
        }
    }
}