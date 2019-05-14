using Plus.Dependency;
using System;

namespace Plus.Configuration.Startup
{
    /// <summary>
    /// 在启动时的模块配置
    /// </summary>
    public interface IPlusStartupConfiguration
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
    }
}