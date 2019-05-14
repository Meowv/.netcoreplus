using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Plus.Dependency
{
    public class IocManager : IIocManager, IIocRegistrar, IIocResolver, IDisposable
    {
        private readonly List<IDependencyRegistrar> _conventionalRegistrars;

        public IWindsorContainer IocContainer { get; private set; }

        public static IocManager Instance { get; private set; }

        static IocManager()
        {
            Instance = new IocManager();
        }

        public IocManager()
        {
            IocContainer = new WindsorContainer();
            _conventionalRegistrars = new List<IDependencyRegistrar>();
            IocContainer.Register(
                Component
                    .For<IocManager, IIocManager, IIocRegistrar, IIocResolver>()
                    .Instance(this)
            );
        }

        public void AddRegistrar(IDependencyRegistrar registrar)
        {
            _conventionalRegistrars.Add(registrar);
        }

        public void RegisterAssembly(Assembly assembly)
        {
            var context = new RegistrationContext(assembly, this);
            foreach (IDependencyRegistrar conventionalRegistrar in _conventionalRegistrars)
            {
                conventionalRegistrar.RegisterAssembly(context);
            }
        }

        public void Register<TType>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton) where TType : class
        {
            IocContainer.Register(ApplyLifestyle(Component.For<TType>(), lifeStyle));
        }

        public void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            IocContainer.Register(ApplyLifestyle(Component.For(type), lifeStyle));
        }

        public void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton) where TType : class where TImpl : class, TType
        {
            IocContainer.Register(ApplyLifestyle(Component.For<TType, TImpl>().ImplementedBy<TImpl>(), lifeStyle));
        }

        public void Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            IocContainer.Register(ApplyLifestyle(Component.For(type, impl).ImplementedBy(impl), lifeStyle));
        }

        public bool IsRegistered(Type type)
        {
            return IocContainer.Kernel.HasComponent(type);
        }

        public bool IsRegistered<TType>()
        {
            return IocContainer.Kernel.HasComponent(typeof(TType));
        }

        public T Resolve<T>()
        {
            return IocContainer.Resolve<T>();
        }

        public T Resolve<T>(Type type)
        {
            return (T)IocContainer.Resolve(type);
        }

        public object Resolve(Type type)
        {
            return IocContainer.Resolve(type);
        }

        public T[] ResolveAll<T>()
        {
            return IocContainer.ResolveAll<T>();
        }

        public object[] ResolveAll(Type type)
        {
            return IocContainer.ResolveAll(type).Cast<object>().ToArray();
        }

        public void Release(object obj)
        {
            IocContainer.Release(obj);
        }

        public void Dispose()
        {
            IocContainer.Dispose();
        }

        private static ComponentRegistration<T> ApplyLifestyle<T>(ComponentRegistration<T> registration, DependencyLifeStyle lifeStyle) where T : class
        {
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Transient:
                    return registration.LifestyleTransient();
                case DependencyLifeStyle.Singleton:
                    return registration.LifestyleSingleton();
                default:
                    return registration;
            }
        }
    }
}