using Plus.Dependency;

namespace Plus.Event
{
    public interface IEventPublisher : ITransientDependency
    {
        void Publish<T>(T eventMessage);
    }
}