using Plus.Event.Bus;
using System.Collections.Generic;

namespace Plus.Domain.Entities
{
    /// <summary>
    /// IGeneratesDomainEvents
    /// </summary>
    public interface IGeneratesDomainEvents
    {
        ICollection<IEventData> DomainEvents { get; }
    }
}