using PayT.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayT.Infrastructure.EventPublisher
{
    public interface IEventHandler<T> where T : IEvent
    {
        public Task HandleAsync(T @event);
    }
}
