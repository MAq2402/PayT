using System;
using System.Collections.Generic;
using System.Text;

namespace PayT.Application.ReadModels
{
    public class SubjectReadModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public IEnumerable<BillReadModel> Bills { get; set; } = new List<BillReadModel>();
    }
}
