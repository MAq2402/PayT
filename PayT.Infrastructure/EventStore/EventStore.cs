using EventStore.ClientAPI;
using Newtonsoft.Json;
using PayT.Domain.Events;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PayT.Infrastructure.Types;

namespace PayT.Infrastructure.EventStore
{
    public class EventStore : IEventStore
    {
        private Settings _settings;

        public EventStore(Settings settings)
        {
            _settings = settings;
        }

        public async Task WriteEventAsync(IEvent @event)
        {
            var conn = EventStoreConnection.Create(new Uri(_settings.EventStoreSettings.Uri));
            await conn.ConnectAsync();
            var streamName = _settings.EventStoreSettings.Stream;
            var eventType = @event.GetType().ToString();
            var metadata = string.Empty;
            var eventPayload = new EventData(Guid.NewGuid(), eventType, true,
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event).ToCharArray()),
                Encoding.UTF8.GetBytes(metadata));
            conn.AppendToStreamAsync(streamName, ExpectedVersion.Any, eventPayload).Wait();
        }

        public async Task<IEnumerable<IEvent>> ReadEventsAsync(Guid aggregateRootId)
        {
            var conn = EventStoreConnection.Create(new Uri(_settings.EventStoreSettings.Uri));
            await conn.ConnectAsync();
            var streamName = _settings.EventStoreSettings.Stream;

            var eventStream = await conn.ReadStreamEventsForwardAsync(streamName, 0, 1000, true);
            var result = new List<IEvent>();

            foreach (var resolvedEvent in eventStream.Events)
            {
                var assembly = Assembly.GetAssembly(typeof(Event));
                var eventType = assembly.GetType(resolvedEvent.Event.EventType);
                if (eventType != null)
                {
                    var @event = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(resolvedEvent.Event.Data), eventType) as IEvent;
                    if (@event.AggregateRootId == aggregateRootId)
                    {
                        result.Add(@event);
                    }
                }
            }
            return result;
        }
    }
}
