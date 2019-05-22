using Plus.Dependency;
using System.Collections.Generic;

namespace Plus.Event
{
    public interface ISubscriptionService : ITransientDependency
    {
        IList<IConsumer<T>> GetSubscriptions<T>();
    }
}