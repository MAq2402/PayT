using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayT.Application.Commands
{
    public class CreateSubjectCommand : IRequest
    {
        public CreateSubjectCommand(string name, decimal amount)
        {
            Name = name;
            Amount = amount;
        }

        public string Name { get; private set; }
        public decimal Amount { get; private set; }
    }
}
