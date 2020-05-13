using PayT.Domain.Entities;
using PayT.Domain.Repositories;
using PayT.Infrastructure.EventPublisher;
using PayT.Infrastructure.EventStore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayT.Infrastructure.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : AggregateRoot
    {
        private IEventStore _eventStore;
        private IEventPublisher _eventPublisher;

        public WriteRepository(IEventStore eventStore, IEventPublisher eventPublisher)
        {
            _eventStore = eventStore;
            _eventPublisher = eventPublisher;
        }

        public async Task CommitAsync(T aggregateRoot)
        {
            foreach(var @event in aggregateRoot.UncomittedEvents)
            {
                await _eventStore.WriteEventAsync(@event);
                await _eventPublisher.PublishAsync(@event);
            }

            aggregateRoot.ClearEvents();
        }

        public async Task<T> GetSingleByIdAsync(Guid id)
        {
            var events = await _eventStore.ReadEventsAsync(id);

            var aggregateRoot = CreateEmptyAggregateRoot<T>();

            foreach(var @event in events)
            {
                aggregateRoot.On(@event);
            }

            return aggregateRoot;
        }

        private TAggregate CreateEmptyAggregateRoot<TAggregate>() where TAggregate : AggregateRoot
        {
            return (TAggregate)Activator.CreateInstance(typeof(TAggregate), nonPublic: true);
        }
    }
}
