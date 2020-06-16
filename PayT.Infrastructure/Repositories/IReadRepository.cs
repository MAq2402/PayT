using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PayT.Infrastructure.Types;

namespace PayT.Infrastructure.Repositories
{
    public interface IReadRepository<T> where T : IReadModel
    {
        Task InsertOneAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
