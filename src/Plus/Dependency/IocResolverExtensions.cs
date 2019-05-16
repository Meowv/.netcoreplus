using System;

namespace Plus.Dependency
{
    /// <summary>
    /// <see cref="IIocResolver"/> 扩展方法
    /// </summary>
    public static class IocResolverExtensions
    {
        public static IDisposableDependencyObjectWrapper<T> ResolveAsDisposable<T>(this IIocResolver iocResolver)
        {
            return new DisposableDependencyObjectWrapper<T>(iocResolver, iocResolver.Resolve<T>());
        }

        public static IDisposableDependencyObjectWrapper<T> ResolveAsDisposable<T>(this IIocResolver iocResolver, Type type)
        {
            return new DisposableDependencyObjectWrapper<T>(iocResolver, (T)iocResolver.Resolve(type));
        }

        public static IDisposableDependencyObjectWrapper ResolveAsDisposable(this IIocResolver iocResolver, Type type)
        {
            return new DisposableDependencyObjectWrapper(iocResolver, iocResolver.Resolve(type));
        }
    }
}