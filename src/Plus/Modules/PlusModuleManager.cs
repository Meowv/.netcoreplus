using Castle.Core.Logging;
using Plus.Configuration.Startup;
using Plus.Dependency;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Plus.Modules
{
    /// <summary>
    /// 模块管理
    /// </summary>
    public class PlusModuleManager : IPlusModuleManager
    {
        public PlusModuleInfo StartupModule { get; private set; }

        public IReadOnlyList<PlusModuleInfo> Modules => _modules.ToImmutableList();

        public ILogger Logger { get; set; }

        private PlusModuleCollection _modules;

        private readonly IIocManager _iocManager;

        private readonly IPlusStartupConfiguration _startupConfiguration;

        public PlusModuleManager(IIocManager iocManager, IPlusStartupConfiguration startupConfiguration)
        {
            _iocManager = iocManager;
            _startupConfiguration = startupConfiguration;
            Logger = NullLogger.Instance;
        }

        public virtual void Initialize(Type startupModule)
        {
            _modules = new PlusModuleCollection(startupModule);
            LoadAllModules();
        }

        public virtual void StartModules()
        {
            var sortedModules = _modules.GetSortedModuleListByDependency();
            sortedModules.ForEach(module => module.Instance.PreInitialize());
            sortedModules.ForEach(module => module.Instance.Initialize());
            sortedModules.ForEach(module => module.Instance.PostInitialize());
        }

        public virtual void ShutdownModules()
        {
            Logger.Debug("开始关闭模块");
            List<PlusModuleInfo> sortedModuleListByDependency = _modules.GetSortedModuleListByDependency();
            sortedModuleListByDependency.Reverse();
            sortedModuleListByDependency.ForEach(delegate (PlusModuleInfo sm)
            {
                sm.Instance.Shutdown();
            });
            Logger.Debug("模块关闭完成");
        }

        private void LoadAllModules()
        {
            Logger.Debug("加载模块...");

            var moduleTypes = FindAllModuleTypes().Distinct().ToList();

            Logger.Debug("找到 " + moduleTypes.Count + " 个模块.");

            RegisterModules(moduleTypes);
            CreateModules(moduleTypes);

            _modules.EnsureKernelModuleToBeFirst();
            _modules.EnsureStartupModuleToBeLast();

            SetDependencies();

            Logger.DebugFormat("{0} 个模块已加载.", _modules.Count);
        }

        private List<Type> FindAllModuleTypes()
        {
            return PlusModule.FindDependedModuleTypesRecursivelyIncludingGivenModule(_modules.StartupModuleType);
        }

        private void CreateModules(ICollection<Type> moduleTypes)
        {
            foreach (var moduleType in moduleTypes)
            {
                if (!(_iocManager.Resolve(moduleType) is PlusModule moduleObject))
                {
                    throw new PlusInitializationException("从类型不是 Plus 模块: " + moduleType.AssemblyQualifiedName);
                }

                moduleObject.IocManager = _iocManager;
                moduleObject.Configuration = _iocManager.Resolve<IPlusStartupConfiguration>();

                var moduleInfo = new PlusModuleInfo(moduleType, moduleObject);

                _modules.Add(moduleInfo);

                if (moduleType == _modules.StartupModuleType)
                {
                    StartupModule = moduleInfo;
                }

                Logger.DebugFormat("加载模块: " + moduleType.AssemblyQualifiedName);
            }
        }

        private void RegisterModules(ICollection<Type> moduleTypes)
        {
            foreach (Type moduleType in moduleTypes)
            {
                _iocManager.RegisterIfNot(moduleType);
            }
        }

        private void SetDependencies()
        {
            foreach (PlusModuleInfo module in _modules)
            {
                module.Dependencies.Clear();
                foreach (Type dependedModuleType in PlusModule.FindDependedModuleTypes(module.Type))
                {
                    PlusModuleInfo PlusModuleInfo = _modules.FirstOrDefault((PlusModuleInfo m) => m.Type == dependedModuleType);
                    if (PlusModuleInfo == null)
                    {
                        throw new PlusInitializationException("无法找到依赖的模块 " + dependedModuleType.AssemblyQualifiedName + " for " + module.Type.AssemblyQualifiedName);
                    }
                    if (module.Dependencies.FirstOrDefault((PlusModuleInfo dm) => dm.Type == dependedModuleType) == null)
                    {
                        module.Dependencies.Add(PlusModuleInfo);
                    }
                }
            }
        }
    }
}