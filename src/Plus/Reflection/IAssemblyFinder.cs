using System.Collections.Generic;
using System.Reflection;

namespace Plus.Reflection
{
    /// <summary>
    /// 获取程序集接口
    /// </summary>
    public interface IAssemblyFinder
    {
        /// <summary>
        /// 获取程序集列表
        /// </summary>
        /// <returns></returns>
        List<Assembly> GetAllAssemblies();
    }
}