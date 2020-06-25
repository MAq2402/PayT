using MediatR;
using Microsoft.AspNetCore.Mvc;
using PayT.Application.Commands;
using PayT.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PayT.Application.Queries;

namespace PayT.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubjectAsync([FromBody] SubjectForCreationDto model)
        {
            await _mediator.Send(new CreateSubjectCommand(
                string.IsNullOrEmpty(model.Id) ? Guid.NewGuid() : new Guid(model.Id), model.Name, model.Amount));

            return new CreatedResult(string.Empty, null);
        }

        [HttpPut("{id}/bills")]
        public async Task<IActionResult> PayBillAsync(string id, [FromBody] BillForCreationDto model)
        {
            await _mediator.Send(new PayBillCommand(new Guid(id), model.Amount));

            return NoContent();
        }

        [HttpGet()]
        public async Task<IActionResult> GetSubjectsAsync()
        {
            var result = await _mediator.Send(new GetSubjectsQuery());

            return Ok(result);
        }
    }
}