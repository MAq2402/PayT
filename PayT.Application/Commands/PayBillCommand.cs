using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace PayT.Application.Commands
{
    public class PayBillCommand : IRequest
    {
        public PayBillCommand(Guid subjectId, decimal amount)
        {
            SubjectId = subjectId;
            Amount = amount;
        }

        public Guid SubjectId { get; private set; }
        public decimal Amount { get; private set; }
    }
}
