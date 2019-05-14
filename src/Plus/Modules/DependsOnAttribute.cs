using System;

namespace Plus.Modules
{
    /// <summary>
    /// 用于定义模块间的依赖关系
    /// </summary>
    public class DependsOnAttribute : Attribute
    {
        public Type[] DependedModuleTypes { get; private set; }

        public DependsOnAttribute(params Type[] dependedModuleTypes)
        {
            DependedModuleTypes = dependedModuleTypes;
        }
    }
}