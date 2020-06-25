using MediatR;
using PayT.Application.Commands;
using PayT.Domain.Entities;
using PayT.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PayT.Application.CommandHandlers
{
    public class CreateSubjectHandler : IRequestHandler<CreateSubjectCommand>
    {
        private IWriteRepository<Subject> _subjectRepository;

        public CreateSubjectHandler(IWriteRepository<Subject> subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<Unit> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = new Subject(request.Id, request.Name, request.Amount);
            await _subjectRepository.CommitAsync(subject);

            return new Unit();
        }
    }
}
