using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PayT.Application.ReadModels;
using PayT.Infrastructure.Repositories;

namespace PayT.Application.Repositories
{
    public interface ISubjectReadRepository : IReadRepository<SubjectReadModel>
    {
        Task InsertBillIntoSubjectAsync(BillReadModel bill, Guid subjectId);
    }
}
