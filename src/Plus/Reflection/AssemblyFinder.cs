using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Plus.Reflection
{
    /// <summary>
    /// AssemblyFinder
    /// </summary>
    public class AssemblyFinder : IAssemblyFinder
    {
        //private readonly IPlusModuleManager _moduleManager;

        public static AssemblyFinder Instance
        {
            get;
            private set;
        }

        static AssemblyFinder()
        {
            Instance = new AssemblyFinder();
        }

        //public AssemblyFinder(IPlusModuleManager moduleManager)
        //{
        //    _moduleManager = moduleManager;
        //}

        public List<Assembly> GetAllAssemblies()
        {
            //var assemblies = new List<Assembly>();

            //foreach (var module in _moduleManager.Modules)
            //{
            //    assemblies.Add(module.Assembly);
            //    assemblies.AddRange(module.Instance.GetAdditionalAssemblies());
            //}

            //return assemblies.Distinct().ToList();

            return AppDomain.CurrentDomain.GetAssemblies().ToList();
        }
    }
}