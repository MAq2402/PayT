using PayT.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayT.Domain.Repositories
{
    public interface IRepository<T> where T : AggregateRoot
    {
        Task CommitAsync(T aggregateRoot);
        Task<T> GetSingleByIdAsync(Guid aggregateRootID);
    }
}
