using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayT.Application.Commands
{
    public class CreateSubjectCommand : IRequest
    {
        public CreateSubjectCommand(Guid? id, string name, decimal amount)
        {
            Id = id;
            Name = name;
            Amount = amount;
        }

        public Guid? Id { get; set; }
        public string Name { get; private set; }
        public decimal Amount { get; private set; }
    }
}
