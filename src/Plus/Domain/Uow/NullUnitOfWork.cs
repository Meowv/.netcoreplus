using System.Threading.Tasks;

namespace Plus.Domain.Uow
{
    /// <summary>
    /// 工作单元的空实现
    /// </summary>
    public sealed class NullUnitOfWork : UnitOfWorkBase
    {
        public override void SaveChanges()
        {

        }

        public override Task SaveChangesAsync()
        {
            return Task.FromResult(0);
        }

        protected override void BeginUow()
        {

        }

        protected override void CompleteUow()
        {

        }

        protected override Task CompleteUowAsync()
        {
            return Task.FromResult(0);
        }

        protected override void DisposeUow()
        {

        }

        public NullUnitOfWork(IConnectionStringResolver connectionStringResolver, IUnitOfWorkDefaultOptions defaultOptions, IUnitOfWorkFilterExecuter filterExecuter)
            : base(connectionStringResolver, defaultOptions, filterExecuter)
        {
        }
    }
}