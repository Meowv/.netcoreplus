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
            return this.SortByDependencies((PlusModuleInfo x) => x.Dependencies);
        }

        public void EnsureLeadershipToBeFirst()
        {
            EnsureLeadershipToBeFirst(this);
        }

        public void EnsureStartupModuleToBeLast()
        {
            EnsureStartupModuleToBeLast(this, StartupModuleType);
        }

        public static void EnsureLeadershipToBeFirst(List<PlusModuleInfo> modules)
        {
            int num = modules.FindIndex((PlusModuleInfo x) => x.Type == typeof(PlusLeadershipModule));
            if (num > 0)
            {
                PlusModuleInfo item = modules[num];
                modules.RemoveAt(num);
                modules.Insert(0, item);
            }
        }

        public static void EnsureStartupModuleToBeLast(List<PlusModuleInfo> modules, Type startupModuleType)
        {
            int num = modules.FindIndex((PlusModuleInfo x) => x.Type == startupModuleType);
            if (num < modules.Count - 1)
            {
                PlusModuleInfo item = modules[num];
                modules.RemoveAt(num);
                modules.Add(item);
            }
        }
    }
}