using MongoDB.Driver;
using Plus.Dependency;
using Plus.Domain.Uow;
using Plus.MongoDb.Configuration;
using System.Threading.Tasks;

namespace Plus.MongoDb.Uow
{
    /// <summary>
    /// 工作单元
    /// TODO...
    /// </summary>
    public class MongoDbUnitOfWork : UnitOfWorkBase, ITransientDependency
    {
        public MongoDatabase Database { get; private set; }

        private readonly IMongoDbModuleConfiguration _configuration;

        public MongoDbUnitOfWork(
            IMongoDbModuleConfiguration configuration,
            IConnectionStringResolver connectionStringResolver,
            IUnitOfWorkFilterExecuter filterExecuter,
            IUnitOfWorkDefaultOptions defaultOptions)
            : base(
                  connectionStringResolver,
                  defaultOptions,
                  filterExecuter)
        {
            _configuration = configuration;
        }

        public override void SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public override Task SaveChangesAsync()
        {
            throw new System.NotImplementedException();
        }

        protected override void CompleteUow()
        {
            throw new System.NotImplementedException();
        }

        protected override Task CompleteUowAsync()
        {
            throw new System.NotImplementedException();
        }

        protected override void DisposeUow()
        {
            throw new System.NotImplementedException();
        }
    }
}