using System;
using System.Collections.Generic;
using System.Text;
using PayT.Infrastructure.Types;

namespace PayT.Application.ReadModels
{
    public class SubjectReadModel : IReadModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public IList<BillReadModel> Bills { get; set; } = new List<BillReadModel>();
    }
}
