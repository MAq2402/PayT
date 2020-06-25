using PayT.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayT.Domain.Entities
{
    public class Subject : AggregateRoot
    {
        private List<Bill> _bills = new List<Bill>();

        public Subject(Guid? id, string name, decimal amount)
        {
            RaiseEvent(new SubjectCreatedEvent(id ?? Guid.NewGuid(), name, amount));
        }

        private Subject()
        {
        }

        public string Name { get; private set; }
        public decimal Amount { get; private set; }
        public IEnumerable<Bill> Bills => _bills;

        public void Pay(decimal amount)
        {
            RaiseEvent(new BillPayedEvent(Id, Guid.NewGuid(), amount, DateTime.UtcNow));
        }

        public void On(SubjectCreatedEvent @event)
        {
            Id = @event.AggregateRootId;
            Name = @event.Name;
            Amount = @event.Amount;
        }

        public void On(BillPayedEvent @event)
        {
            _bills.Add(new Bill(@event.Id, @event.Amount, @event.DateTime));
        }
    }
}