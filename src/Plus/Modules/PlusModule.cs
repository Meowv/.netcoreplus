using Castle.Core.Logging;
using Plus.Collections;
using Plus.Configuration.Startup;
using Plus.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Plus.Modules
{
    public abstract class PlusModule
    {
        protected internal IIocManager IocManager { get; internal set; }

        protected internal IPlusStartupConfiguration Configuration { get; internal set; }

        public ILogger Logger { get; set; }

        protected PlusModule()
        {
            Logger = NullLogger.Instance;
        }

        /// <summary>
        /// 启动时初始化事件，在依赖注入注册之前运行。
        /// </summary>
        public virtual void PreInitialize()
        {

        }

        /// <summary>
        /// 用于注册此模块的依赖项
        /// </summary>
        public virtual void Initialize()
        {

        }

        /// <summary>
        /// 最后在应用程序启动时调用此方法
        /// </summary>
        public virtual void PostInitialize()
        {

        }

        /// <summary>
        /// 在关闭应用程序时调用此方法
        /// </summary>
        public virtual void Shutdown()
        {

        }

        public virtual Assembly[] GetAdditionalAssemblies()
        {
            return new Assembly[0];
        }

        public static bool IsPlusModule(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            return
                typeInfo.IsClass &&
                !typeInfo.IsAbstract &&
                !typeInfo.IsGenericType &&
                typeof(PlusModule).IsAssignableFrom(type);
        }

        /// <summary>
        /// 查找模块的依赖模块
        /// </summary>
        public static List<Type> FindDependedModuleTypes(Type moduleType)
        {
            if (!IsPlusModule(moduleType))
            {
                throw new PlusInitializationException("This type is not an Plus module: " + moduleType.AssemblyQualifiedName);
            }

            var list = new List<Type>();

            if (moduleType.GetTypeInfo().IsDefined(typeof(DependsOnAttribute), true))
            {
                var dependsOnAttributes = moduleType.GetTypeInfo().GetCustomAttributes(typeof(DependsOnAttribute), true).Cast<DependsOnAttribute>();
                foreach (var dependsOnAttribute in dependsOnAttributes)
                {
                    foreach (var dependedModuleType in dependsOnAttribute.DependedModuleTypes)
                    {
                        list.Add(dependedModuleType);
                    }
                }
            }

            return list;
        }

        public static List<Type> FindDependedModuleTypesRecursivelyIncludingGivenModule(Type moduleType)
        {
            var list = new List<Type>();
            AddModuleAndDependenciesRecursively(list, moduleType);
            list.AddIfNotContains(typeof(PlusLeadershipModule));
            return list;
        }

        private static void AddModuleAndDependenciesRecursively(List<Type> modules, Type module)
        {
            if (!IsPlusModule(module))
            {
                throw new PlusInitializationException("This type is not an Plus module: " + module.AssemblyQualifiedName);
            }

            if (modules.Contains(module))
            {
                return;
            }

            modules.Add(module);

            var dependedModules = FindDependedModuleTypes(module);
            foreach (var dependedModule in dependedModules)
            {
                AddModuleAndDependenciesRecursively(modules, dependedModule);
            }
        }
    }
}