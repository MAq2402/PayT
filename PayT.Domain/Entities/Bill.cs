using System;
using System.Collections.Generic;
using System.Text;

namespace PayT.Domain.Entities
{
    public class Bill : Entity
    {
        public Bill(Guid id, decimal amount, DateTime dateTime) : base(id)
        {
            Amount = amount;
            DateTime = dateTime;
        }

        private Bill()
        {

        }

        public decimal Amount { get; private set; }
        public DateTime DateTime { get; private set; }
    }
}
