using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PayT.Domain.Events;
using PayT.Infrastructure.Events;

namespace PayT.Application.EventHandlers
{
    public class DoSthElseWithSubjectHandler : IEventHandler<SubjectCreatedEvent>
    {
        public async Task HandleAsync(SubjectCreatedEvent @event)
        {
            await Task.FromResult(0);
        }
    }
}
