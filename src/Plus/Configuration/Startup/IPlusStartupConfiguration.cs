using Plus.Dependency;
using Plus.Domain.Uow;
using Plus.Runtime.Caching.Configuration;
using System;

namespace Plus.Configuration.Startup
{
    /// <summary>
    /// 在启动时的模块配置
    /// </summary>
    public interface IPlusStartupConfiguration: IDictionaryBasedConfig
    {
        /// <summary>
        /// 获取与此配置关联的IOC管理器
        /// </summary>
        IIocManager IocManager { get; }

        /// <summary>
        /// 用于替换服务类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="replaceAction"></param>
        void ReplaceService(Type type, Action replaceAction);

        /// <summary>
        /// 获取配置对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Get<T>();

        /// <summary>
        /// 用于配置工作单元
        /// </summary>
        IUnitOfWorkDefaultOptions UnitOfWork { get; }

        /// <summary>
        /// 用于配置缓存
        /// </summary>
        ICachingConfiguration Caching { get; }

        /// <summary>
        /// 用于配置验证
        /// </summary>
        IValidationConfiguration Validation { get; }
    }
}