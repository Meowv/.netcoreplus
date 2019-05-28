using Plus.Domain.Uow;
using Plus.Event.Bus;
using Plus.Reflection;

namespace Plus.Services.Test
{
    public abstract class ServiceBase : ApplicationServiceBase
    {
        public EventBus EventBus => EventBus.Default;
        public IUnitOfWorkManager UnitOfWorkManager;
        public ITypeFinder TypeFinder;

        protected ServiceBase()
        {
            UnitOfWorkManager = PlusEngine.Instance.Resolve<IUnitOfWorkManager>();
            TypeFinder = PlusEngine.Instance.Resolve<ITypeFinder>();
        }
    }
}