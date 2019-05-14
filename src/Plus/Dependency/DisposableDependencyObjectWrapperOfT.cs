using System;

namespace Plus.Dependency
{
    internal class DisposableDependencyObjectWrapper<T> : IDisposableDependencyObjectWrapper<T>, IDisposable
    {
        private readonly IIocResolver _iocResolver;

        public T Object { get; private set; }

        public DisposableDependencyObjectWrapper(IIocResolver iocResolver, T obj)
        {
            _iocResolver = iocResolver;
            Object = obj;
        }

        public void Dispose()
        {
            _iocResolver.Release(Object);
        }
    }
}