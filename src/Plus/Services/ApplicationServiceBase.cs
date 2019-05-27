using Castle.Core.Logging;
using Plus.Dependency;
using Plus.Event;

namespace Plus.Services
{
    public abstract class ApplicationServiceBase : IApplicationService, ITransientDependency
    {
        public ILogger Logger { protected get; set; }

        public IEventPublisher EventPublisher { protected get; set; }

        protected ApplicationServiceBase()
        {
            Logger = NullLogger.Instance;
            EventPublisher = PlusEngine.Instance.Resolve<IEventPublisher>();
        }
    }
}