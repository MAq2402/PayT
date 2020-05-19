using PayT.Domain.Events;
using PayT.Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PayT.Application.ReadModels;
using PayT.Infrastructure.Repositories;

namespace PayT.Application.EventHandlers
{
    public class InsertSubjectIntoReadModelHandler : IEventHandler<SubjectCreatedEvent>
    {
        private IReadRepository _readRepository;

        public InsertSubjectIntoReadModelHandler(IReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task HandleAsync(SubjectCreatedEvent @event)
        {
            _readRepository.InsertOne(new SubjectReadModel
            {
                Id = @event.AggregateRootId,
                Amount = @event.Amount,
                Name = @event.Name
            });

            await Task.FromResult(0);
        }
    }
}