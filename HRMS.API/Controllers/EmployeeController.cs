using HRMS.Application.Employees.Commands;
using HRMS.Application.Employees.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IEmployeeRepository _repository;
        public EmployeeController(IMediator mediator, IEmployeeRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _repository.GetAllEmployees());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _repository.GetEmployeeById(id);
            return employee != null ? Ok(employee) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddEmployeeCommand command)
        {
            var employeeId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = employeeId }, command);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteEmployee(id);
            return NoContent();
        }

        [HttpPut("update-employee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("update-employee-status")]
        public async Task<IActionResult> UpdateEmployeeStatus([FromBody] UpdateEmployeeStatusCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("update-job-history-status")]
        public async Task<IActionResult> UpdateJobHistoryStatus([FromBody] UpdateJobHistoryStatusCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}



