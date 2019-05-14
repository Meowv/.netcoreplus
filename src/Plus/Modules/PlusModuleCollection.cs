using Plus.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plus.Modules
{
    /// <summary>
    /// 将对象存储为字典
    /// </summary>
    public class PlusModuleCollection : List<PlusModuleInfo>
    {
        public Type StartupModuleType { get; }

        public PlusModuleCollection(Type startupModuleType)
        {
            StartupModuleType = startupModuleType;
        }

        public TModule GetModule<TModule>() where TModule : PlusModule
        {
            var plusModuleInfo = this.FirstOrDefault((PlusModuleInfo x) => x.Type == typeof(TModule));
            if (plusModuleInfo == null)
            {
                throw new PlusException("Can not find module for " + typeof(TModule).FullName);
            }
            return (TModule)plusModuleInfo.Instance;
        }

        public List<PlusModuleInfo> GetSortedModuleListByDependency()
        {
            var sortedModules = this.SortByDependencies(x => x.Dependencies);
            EnsureKernelModuleToBeFirst(sortedModules);
            EnsureStartupModuleToBeLast(sortedModules, StartupModuleType);
            return sortedModules;
        }

        public static void EnsureKernelModuleToBeFirst(List<PlusModuleInfo> modules)
        {
            var kernelModuleIndex = modules.FindIndex(m => m.Type == typeof(PlusLeadershipModule));
            if (kernelModuleIndex <= 0)
            {
                return;
            }

            var kernelModule = modules[kernelModuleIndex];
            modules.RemoveAt(kernelModuleIndex);
            modules.Insert(0, kernelModule);
        }

        public static void EnsureStartupModuleToBeLast(List<PlusModuleInfo> modules, Type startupModuleType)
        {
            var startupModuleIndex = modules.FindIndex(m => m.Type == startupModuleType);
            if (startupModuleIndex >= modules.Count - 1)
            {
                return;
            }

            var startupModule = modules[startupModuleIndex];
            modules.RemoveAt(startupModuleIndex);
            modules.Add(startupModule);
        }

        public void EnsureKernelModuleToBeFirst()
        {
            EnsureKernelModuleToBeFirst(this);
        }

        public void EnsureStartupModuleToBeLast()
        {
            EnsureStartupModuleToBeLast(this, StartupModuleType);
        }
    }
}