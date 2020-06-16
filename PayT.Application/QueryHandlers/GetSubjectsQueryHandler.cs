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
        private IReadRepository<SubjectReadModel> _readRepository;

        public GetSubjectsQueryHandler(IReadRepository<SubjectReadModel> readRepository)
        {
            _readRepository = readRepository;
        }
        public async Task<IEnumerable<SubjectReadModel>> Handle(GetSubjectsQuery request, CancellationToken cancellationToken)
        {
            return await _readRepository.GetAllAsync();
        }
    }
}
