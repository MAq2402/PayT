using Autofac;
using PayT.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayT.Infrastructure.EventPublisher
{
    public class EventPublisher : IEventPublisher
    {
        private IComponentContext _context;

        public EventPublisher(IComponentContext context)
        {
            _context = context;
        }

        public async Task PublishAsync(IEvent @event) 
        {
            var handlerType = typeof(IEventHandler<>)
                    .MakeGenericType(@event.GetType());

            dynamic handler = _context.Resolve(handlerType);

            await handler.HandleAsync((dynamic)@event);
        }
    }
}
