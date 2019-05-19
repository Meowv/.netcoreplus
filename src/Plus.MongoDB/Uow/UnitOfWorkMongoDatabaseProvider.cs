using MongoDB.Driver;
using Plus.Dependency;
using Plus.Domain.Uow;

namespace Plus.MongoDb.Uow
{
    /// <summary>
    /// Implements <see cref="IMongoDatabaseProvider"/> that gets database from active unit of work.
    /// </summary>
    public class UnitOfWorkMongoDatabaseProvider : IMongoDatabaseProvider, ITransientDependency
    {
        public MongoDatabase Database
        {
            get
            {
                return ((MongoDbUnitOfWork)_currentUnitOfWork.Current).Database;
            }
        }

        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWork;

        public UnitOfWorkMongoDatabaseProvider(ICurrentUnitOfWorkProvider currentUnitOfWork)
        {
            _currentUnitOfWork = currentUnitOfWork;
        }
    }
}