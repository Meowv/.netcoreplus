using System;

namespace Plus.Runtime.Caching
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class CachingAttribute : Attribute
    {
        public string CacheManagerProviderServiceName { get; set; }

        public CachingBehavior Behavior { get; set; }

        public string[] MethodNames { get; set; }

        public CachingAttribute()
            : this("", CachingBehavior.Get, (string[])null)
        {
        }

        public CachingAttribute(string cacheManagerProviderServiceName, CachingBehavior behavior)
            : this(cacheManagerProviderServiceName, behavior, (string[])null)
        {
        }

        public CachingAttribute(string cacheManagerProviderServiceName, CachingBehavior behavior, params string[] methodNames)
        {
            Behavior = behavior;
            CacheManagerProviderServiceName = cacheManagerProviderServiceName;
            MethodNames = methodNames;
        }
    }
}