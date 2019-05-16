using Plus.Dependency;
using Plus.Domain.Uow;
using Plus.Runtime.Caching.Configuration;
using System;
using System.Collections.Generic;

namespace Plus.Configuration.Startup
{
    /// <summary>
    /// 该类用于在启动时配置模块
    /// </summary>
    public class PlusStartupConfiguration : DictionaryBasedConfig, IPlusStartupConfiguration, IDictionaryBasedConfig
    {
        public IIocManager IocManager { get; private set; }

        public Dictionary<Type, Action> ServiceReplaceActions { get; private set; }

        public IUnitOfWorkDefaultOptions UnitOfWork { get; private set; }

        public ICachingConfiguration Caching { get; private set; }

        public IValidationConfiguration Validation { get; private set; }

        public PlusStartupConfiguration(IIocManager iocManager)
        {
            IocManager = iocManager;
        }

        public void Initialize()
        {
            UnitOfWork = IocManager.Resolve<IUnitOfWorkDefaultOptions>();
            Caching = IocManager.Resolve<ICachingConfiguration>();
            Validation = IocManager.Resolve<IValidationConfiguration>();
            ServiceReplaceActions = new Dictionary<Type, Action>();
        }

        public void ReplaceService(Type type, Action replaceAction)
        {
            ServiceReplaceActions[type] = replaceAction;
        }

        public T Get<T>()
        {
            return GetOrCreate(typeof(T).FullName, () => IocManager.Resolve<T>());
        }
    }
}