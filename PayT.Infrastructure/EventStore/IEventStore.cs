using PayT.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayT.Infrastructure.EventStore
{
    public interface IEventStore
    {
        Task WriteEventAsync(IEvent @event);
        Task<IEnumerable<IEvent>> ReadEventsAsync(Guid aggregateRootId);
    }
}
