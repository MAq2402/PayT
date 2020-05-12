using PayT.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayT.Domain.Entities
{
    public class Subject : AggregateRoot
    {
        private List<Bill> _bills = new List<Bill>();

        public Subject(Guid id, string name, decimal amount) : base(id)
        {
            RaiseEvent(new SubjectCreatedEvent(id, name, amount));
        }

        private Subject()
        {
            
        }

        public string Name { get; private set; }
        public decimal Amount { get; private set; }
        public IEnumerable<Bill> Bils => _bills;

        public void Pay(decimal amount)
        {
            _bills.Add(new Bill(Guid.NewGuid(), amount, DateTime.UtcNow));
        }

        public void On(SubjectCreatedEvent @event)
        {
            Name = @event.Name;
            Amount = @event.Amount;
        }
    }
}
