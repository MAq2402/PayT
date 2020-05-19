using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PayT.Application.Queries;
using PayT.Application.ReadModels;
using PayT.Infrastructure.Repositories;

namespace PayT.Application.QueryHandlers
{
    public class GetSubjectsQueryHandler : IRequestHandler<GetSubjectsQuery, IEnumerable<SubjectReadModel>>
    {
        private IReadRepository _readRepository;

        public GetSubjectsQueryHandler(IReadRepository readRepository)
        {
            _readRepository = readRepository;
        }
        public Task<IEnumerable<SubjectReadModel>> Handle(GetSubjectsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_readRepository.GetAll<SubjectReadModel>());
        }
    }
}
