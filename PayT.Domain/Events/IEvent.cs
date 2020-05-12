using System;
using System.Collections.Generic;
using System.Text;

namespace PayT.Domain.Events
{
    public interface IEvent
    {
        Guid AggregateRootId { get; }
    }
}
