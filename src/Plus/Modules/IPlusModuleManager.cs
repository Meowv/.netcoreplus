using System;
using System.Collections.Generic;

namespace Plus.Modules
{
    /// <summary>
    /// 模块管理接口
    /// </summary>
    public interface IPlusModuleManager
    {
        PlusModuleInfo StartupModule { get; }

        IReadOnlyList<PlusModuleInfo> Modules { get; }

        void Initialize(Type startupModule);

        void StartModules();

        void ShutdownModules();
    }
}