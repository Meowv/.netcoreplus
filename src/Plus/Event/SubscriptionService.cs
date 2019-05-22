using Plus.Dependency;
using System.Collections.Generic;

namespace Plus.Event
{
    public class SubscriptionService : ISubscriptionService, ITransientDependency
    {
        public IList<IConsumer<T>> GetSubscriptions<T>()
        {
            return PlusEngine.Instance.IocManager.ResolveAll<IConsumer<T>>();
        }
    }
}