using System;

namespace Plus.Domain.Entities.Auditing
{
    /// <summary>
    /// IHasCreationTime
    /// </summary>
    public interface IHasCreationTime
    {
        DateTime CreationTime { get; set; }
    }
}