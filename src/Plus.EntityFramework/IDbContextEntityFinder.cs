using Plus.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Plus.EntityFramework
{
    /// <summary>
    /// IDbContextEntityFinder
    /// </summary>
    public interface IDbContextEntityFinder
    {
        IEnumerable<EntityTypeInfo> GetEntityTypeInfos(Type dbContextType);
    }
}