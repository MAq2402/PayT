using PayT.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayT.Infrastructure.EventPublisher
{
    public interface IEventPublisher
    {
        Task PublishAsync(IEvent @event);
    }
}
