using System;
using System.Reflection;

namespace Plus.Dependency
{
    /// <summary>
    /// 用于注册依赖项的接口
    /// </summary>
    public interface IIocRegistrar
    {
        /// <summary>
        /// Add Registrar
        /// </summary>
        /// <param name="registrar"></param>
        void AddRegistrar(IDependencyRegistrar registrar);

        /// <summary>
        /// Register Assembly
        /// </summary>
        /// <param name="assembly"></param>
        void RegisterAssembly(Assembly assembly);

        /// <summary>
        /// Register
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lifeStyle"></param>
        void Register<T>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton) where T : class;

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="type"></param>
        /// <param name="lifeStyle"></param>
        void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);

        /// <summary>
        /// Register
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <typeparam name="TImpl"></typeparam>
        /// <param name="lifeStyle"></param>
        void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton) where TType : class where TImpl : class, TType;

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="type"></param>
        /// <param name="impl"></param>
        /// <param name="lifeStyle"></param>
        void Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);

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
    }
}