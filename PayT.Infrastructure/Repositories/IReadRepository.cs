using System;
using System.Collections.Generic;
using System.Text;

namespace PayT.Infrastructure.Repositories
{
    public interface IReadRepository
    {
        void InsertOne<T>(T entity);
        IEnumerable<T> GetAll<T>();
    }
}
