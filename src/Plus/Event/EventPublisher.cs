using Castle.Core.Logging;
using Plus.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plus.Event
{
    public class EventPublisher : IEventPublisher, ITransientDependency
    {
        private readonly ISubscriptionService _subscriptionService;

        public ILogger Logger
        {
            get;
            set;
        }

        public EventPublisher(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
            Logger = NullLogger.Instance;
        }

        public virtual void Publish<T>(T eventMessage)
        {
            IList<IConsumer<T>> subscriptions = _subscriptionService.GetSubscriptions<T>();
            subscriptions.ToList().ForEach(delegate (IConsumer<T> x)
            {
                PublishToConsumer(x, eventMessage);
            });
        }

        protected virtual void PublishToConsumer<T>(IConsumer<T> x, T eventMessage)
        {
            try
            {
                x.HandleEvent(eventMessage);
            }
            catch (Exception ex)
            {
                try
                {
                    Logger.Error(ex.Message, ex);
                }
                catch (Exception)
                {
                }
            }
        }
    }
}