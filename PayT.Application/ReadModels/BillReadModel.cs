using System;
using System.Collections.Generic;
using System.Text;

namespace PayT.Application.ReadModels
{
    public class BillReadModel
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateTime { get; set; }
    }
}
