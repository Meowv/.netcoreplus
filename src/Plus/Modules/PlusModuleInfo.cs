using System;
using System.Collections.Generic;
using System.Reflection;

namespace Plus.Modules
{
    /// <summary>
    /// PlusModuleInfo
    /// </summary>
    public class PlusModuleInfo
    {
        public Assembly Assembly { get; }

        public Type Type { get; }

        public PlusModule Instance { get; }

        public bool IsLoadedAsPlugIn { get; }

        public List<PlusModuleInfo> Dependencies { get; }

        public PlusModuleInfo(Type type, PlusModule instance)
        {
            Type = type;
            Instance = instance;
            Assembly = Type.GetTypeInfo().Assembly;
            Dependencies = new List<PlusModuleInfo>();
        }

        public PlusModuleInfo(Type type, PlusModule instance, bool isLoadedAsPlugIn)
        {
            Type = type;
            Instance = instance;
            IsLoadedAsPlugIn = isLoadedAsPlugIn;
            Assembly = Type.GetTypeInfo().Assembly;

            Dependencies = new List<PlusModuleInfo>();
        }

        public override string ToString()
        {
            return Type.AssemblyQualifiedName ?? Type.FullName;
        }
    }
}