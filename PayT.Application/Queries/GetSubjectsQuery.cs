using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using PayT.Application.ReadModels;

namespace PayT.Application.Queries
{
    public class GetSubjectsQuery : IRequest<IEnumerable<SubjectReadModel>>
    {
    }
}
