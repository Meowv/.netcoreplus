using System;

namespace Plus.EntityFramework
{
    /// <summary>
    /// IDbContextTypeMatcher
    /// </summary>
    public interface IDbContextTypeMatcher
    {
        void Populate(Type[] dbContextTypes);

        Type GetConcreteType(Type sourceDbContextType);
    }
}