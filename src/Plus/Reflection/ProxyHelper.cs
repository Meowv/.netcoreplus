using Castle.DynamicProxy;

namespace Plus.Reflection
{
    /// <summary>
    /// ProxyHelper
    /// </summary>
    public class ProxyHelper
    {
        public static object UnProxy(object obj)
        {
            return ProxyUtil.GetUnproxiedInstance(obj);
        }
    }
}