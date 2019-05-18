using System;

namespace Plus.Domain.Entities.Auditing
{
    public interface IHasDeletionTime : ISoftDelete
    {
        DateTime? DeletionTime
        {
            get;
            set;
        }
    }
}