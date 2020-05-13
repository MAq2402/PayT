using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PayT.Application.Commands;
using PayT.Domain.Entities;
using PayT.Infrastructure.Repositories;

namespace PayT.Application.CommandHandlers
{
    public class PayBillHandler : IRequestHandler<PayBillCommand>
    {
        private IWriteRepository<Subject> _subjectRepository;

        public PayBillHandler(IWriteRepository<Subject> subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<Unit> Handle(PayBillCommand request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.GetSingleByIdAsync(request.SubjectId);
            subject.Pay(request.Amount);

            await _subjectRepository.CommitAsync(subject);

            return new Unit();
        }
    }
}
