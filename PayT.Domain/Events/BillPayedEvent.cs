using System;
using System.Collections.Generic;
using System.Text;

namespace PayT.Domain.Events
{
    public class BillPayedEvent : Event
    {
        public BillPayedEvent(Guid aggregateRootId, Guid id, decimal amount, DateTime dateTime) : base(aggregateRootId)
        {
            Id = id;
            Amount = amount;
            DateTime = dateTime;
        }

        public Guid Id { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime DateTime { get; private set; }
    }
}
