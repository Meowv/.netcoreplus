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
        public static AssemblyFinder Instance { get; private set; }

        static AssemblyFinder()
        {
            Instance = new AssemblyFinder();
        }

        public List<Assembly> GetAllAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies().ToList();
        }
    }
}