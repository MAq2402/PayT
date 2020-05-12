using System;
using System.Collections.Generic;
using System.Text;

namespace PayT.Domain.Events
{
    public class SubjectCreatedEvent : Event
    {
        public SubjectCreatedEvent(Guid aggregateRootId ,string name, decimal amount) : base(aggregateRootId)
        {
            Name = name;
            Amount = amount;
        }

        public string Name { get; private set; }
        public decimal Amount { get; private set; }
    }
}
