using Castle.Windsor;
using System;

namespace Plus.Dependency
{
    /// <summary>
    /// 此接口用于直接执行依赖项注入任务
    /// </summary>
    public interface IIocManager : IIocRegistrar, IIocResolver, IDisposable
    {
        /// <summary>
        /// <see cref="IWindsorContainer"/>
        /// </summary>
        IWindsorContainer IocContainer { get; }

        /// <summary>
        /// 是否注册了给定的类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        new bool IsRegistered(Type type);

        /// <summary>
        /// 是否注册了给定的类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        new bool IsRegistered<T>();
    }
}