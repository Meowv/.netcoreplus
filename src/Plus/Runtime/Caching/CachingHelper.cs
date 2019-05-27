using System.Reflection;

namespace Plus.Runtime.Caching
{
    public class CachingHelper
    {
        public static bool HasCachingAttribute(MemberInfo methodInfo)
        {
            return methodInfo.IsDefined(typeof(CachingAttribute), inherit: true);
        }
    }
}