using System;
using System.Collections.Generic;
using System.Text;
using PayT.Infrastructure.Types;

namespace PayT.Application.ReadModels
{
    public class BillReadModel : IReadModel
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateTime { get; set; }
    }
}
