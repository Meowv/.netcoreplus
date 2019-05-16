using Plus.Modules;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Plus.Reflection
{
    public class AssemblyFinder : IAssemblyFinder
    {
        private readonly IPlusModuleManager _moduleManager;

        public AssemblyFinder(IPlusModuleManager moduleManager)
        {
            _moduleManager = moduleManager;
        }

        public List<Assembly> GetAllAssemblies()
        {
            var assemblies = new List<Assembly>();

            foreach (var module in _moduleManager.Modules)
            {
                assemblies.Add(module.Assembly);
                assemblies.AddRange(module.Instance.GetAdditionalAssemblies());
            }

            return assemblies.Distinct().ToList();
        }
    }
}