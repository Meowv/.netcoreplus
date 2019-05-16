using System;

namespace Plus.Reflection
{
    /// <summary>
    /// ITypeFinder
    /// </summary>
    public interface ITypeFinder
    {
        Type[] Find(Func<Type, bool> predicate);

        Type[] FindAll();
    }
}