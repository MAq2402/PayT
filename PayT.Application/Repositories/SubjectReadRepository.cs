using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayT.Application.ReadModels;
using PayT.Infrastructure.Repositories;
using MongoDB.Driver;

namespace PayT.Application.Repositories
{
    public class SubjectReadRepository : ReadRepository<SubjectReadModel>, ISubjectReadRepository
    {
        public async Task InsertBillIntoSubjectAsync(BillReadModel bill, Guid subjectId)
        {
            var result = await _collection.FindAsync(x => x.Id == subjectId);

            var subject = result.Single();

            subject.Bills.Add(bill);

            await _collection.ReplaceOneAsync(x => x.Id == subjectId, subject);
        }
    }
}
