using System;

namespace Plus.Domain.Entities.Auditing
{
    public interface IHasCreationTime
    {
        DateTime CreationTime
        {
            get;
            set;
        }
    }
}
