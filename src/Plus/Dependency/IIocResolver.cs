using System;

namespace Plus.Dependency
{
    /// <summary>
    /// 用于解析依赖项的接口
    /// </summary>
    public interface IIocResolver
    {
        /// <summary>
        /// 从IOC容器中获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Resolve<T>();

        /// <summary>
        /// 从IOC容器中获取对象
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        object Resolve(Type type);

        /// <summary>
        /// 从IOC容器中获取对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="argumentsAsAnonymousType"></param>
        /// <returns></returns>
        object Resolve(Type type, object argumentsAsAnonymousType);

        /// <summary>
        /// 从IOC容器中获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argumentsAsAnonymousType"></param>
        /// <returns></returns>
        T Resolve<T>(object argumentsAsAnonymousType);

        /// <summary>
        /// 从IOC容器中获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T[] ResolveAll<T>();

        /// <summary>
        /// 是否注册了给定的类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool IsRegistered<T>();

        /// <summary>
        /// 是否注册了给定的类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        bool IsRegistered(Type type);

        /// <summary>
        /// 释放预解析对象
        /// </summary>
        /// <param name="obj"></param>
        void Release(object obj);
    }
}