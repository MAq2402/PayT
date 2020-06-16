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
        private readonly IReadRepository<SubjectReadModel> _readRepository;

        public InsertSubjectIntoReadModelHandler(IReadRepository<SubjectReadModel> readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task HandleAsync(SubjectCreatedEvent @event)
        {
            await _readRepository.InsertOneAsync(new SubjectReadModel
            {
                Id = @event.AggregateRootId,
                Amount = @event.Amount,
                Name = @event.Name
            });
        }
    }
}