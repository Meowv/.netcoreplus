using Plus.Dependency;
using System;

namespace Plus
{
    /// <summary>
    /// RegularGuidGenerator
    /// </summary>
    public class RegularGuidGenerator : IGuidGenerator, ITransientDependency
    {
        public virtual Guid Create()
        {
            return Guid.NewGuid();
        }
    }
}