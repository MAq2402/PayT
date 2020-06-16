using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PayT.Application.ReadModels;
using PayT.Application.Repositories;
using PayT.Domain.Events;
using PayT.Infrastructure.Events;
using PayT.Infrastructure.Repositories;

namespace PayT.Application.EventHandlers
{
    public class UpdateSubjectReadModelWithBillHandler : IEventHandler<BillPayedEvent>
    {
        private readonly ISubjectReadRepository _readRepository;

        public UpdateSubjectReadModelWithBillHandler(ISubjectReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task HandleAsync(BillPayedEvent @event)
        {
            await _readRepository.InsertBillIntoSubjectAsync(
                new BillReadModel() {Amount = @event.Amount, DateTime = @event.DateTime, Id = @event.Id},
                @event.AggregateRootId);
        }
    }
}