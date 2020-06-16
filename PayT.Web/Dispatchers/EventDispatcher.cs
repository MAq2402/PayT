using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using PayT.Domain.Events;
using PayT.Infrastructure.Events;

namespace PayT.Web.Dispatchers
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IComponentContext _context;

        public EventDispatcher(IComponentContext context)
        {
            _context = context;
        }

        public IEnumerable DispatchMany(IEvent @event)
        {
            var handlerType = typeof(IEventHandler<>)
                .MakeGenericType(@event.GetType());

            var collectionType = typeof(IEnumerable<>).MakeGenericType(handlerType);

            return _context.IsRegistered(handlerType)
                ? _context.Resolve(collectionType) as IEnumerable
                : new List<object>();
        }
    }
}
