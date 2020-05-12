using PayT.Domain.Events;
using PayT.Infrastructure.EventPublisher;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayT.Application.EventHandlers
{
    public class SubjectCreatedEventHandler : IEventHandler<SubjectCreatedEvent>
    {
        public async Task HandleAsync(SubjectCreatedEvent @event)
        {
            //publisz
        }
    }
}
