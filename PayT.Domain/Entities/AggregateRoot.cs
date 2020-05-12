using PayT.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayT.Domain.Entities
{
    public abstract class AggregateRoot : Entity
    {
        private readonly List<IEvent> _uncomittedEvents = new List<IEvent>();
        public IEnumerable<IEvent> UncomittedEvents => _uncomittedEvents;

        protected AggregateRoot(Guid id) : base(id)
        {
        }

        protected AggregateRoot() : base()
        {
        }

        protected void RaiseEvent(IEvent @event)
        {
            _uncomittedEvents.Add(@event);
            On(@event);
        }

        public void On(IEvent @event)
        {
            ((dynamic)this).On((dynamic)@event);
        }

        public void ClearEvents()
        {
            _uncomittedEvents.Clear();
        }
    }
}
