using MediatR;
using Microsoft.AspNetCore.Mvc;
using PayT.Application.Commands;
using PayT.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayT.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubject([FromBody] SubjectForCreationDto model)
        {
            await _mediator.Send(new CreateSubjectCommand(model.Name, model.Amount));

            return new CreatedResult(string.Empty, null);
        }

        [HttpPut("{id}/bills")]
        public async Task<IActionResult> PayBill(string id, [FromBody] BillForCreationDto model)
        {
            await _mediator.Send(new PayBillCommand(new Guid(id), model.Amount));

            return NoContent();
        }
    }
}
