using PayT.Domain.Entities;
using PayT.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayT.Infrastructure.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : AggregateRoot
    {
    }
}
