using PayT.Domain.Entities;
using PayT.Infrastructure.EventStore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RawRabbit;
using RawRabbit.Context;

namespace PayT.Infrastructure.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : AggregateRoot
    {
        private readonly IEventStore _eventStore;
        private readonly IBusClient<AdvancedMessageContext> _busClient;

        public WriteRepository(IEventStore eventStore, IBusClient<AdvancedMessageContext> busClient)
        {
            _eventStore = eventStore;
            _busClient = busClient;
        }

        public async Task CommitAsync(T aggregateRoot)
        {
            foreach(var @event in aggregateRoot.UncomittedEvents)
            {
                await _eventStore.WriteEventAsync(@event);
                await _busClient.PublishAsync(@event);
            }

            aggregateRoot.ClearEvents();
        }

        public async Task<T> GetSingleByIdAsync(Guid aggregateRootId)
        {
            var events = await _eventStore.ReadEventsAsync(aggregateRootId);

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
