using Plus.Dependency;
using Plus.Event.Bus.Handlers;
using System;

namespace Plus.Event.Bus.Factories
{
    public class IocHandlerFactory : IEventHandlerFactory
    {
        private readonly IIocResolver _iocResolver;

        public Type HandlerType { get; private set; }

        public IocHandlerFactory(IIocResolver iocResolver, Type handlerType)
        {
            _iocResolver = iocResolver;
            HandlerType = handlerType;
        }

        public IEventHandler GetHandler()
        {
            return (IEventHandler)_iocResolver.Resolve(HandlerType);
        }

        public void ReleaseHandler(IEventHandler handler)
        {
            _iocResolver.Release(handler);
        }
    }
}