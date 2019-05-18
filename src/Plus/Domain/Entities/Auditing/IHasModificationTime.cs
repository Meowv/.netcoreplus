using System;

namespace Plus.Domain.Entities.Auditing
{
    public interface IHasModificationTime
    {
        DateTime? LastModificationTime
        {
            get;
            set;
        }
    }
}