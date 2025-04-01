using HRMS.Application.Common.Exceptions;
using HRMS.Application.Employees.Interfaces;
using HRMS.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HRMS.Application.Employees.Commands
{
    public class UpdateEmployeeStatusCommand : IRequest<Unit>
    {
        public int EmployeeId { get; set; }
        public string Status { get; set; }
    }

    public class UpdateEmployeeStatusCommandHandler : IRequestHandler<UpdateEmployeeStatusCommand, Unit>
    {
        private readonly IEmployeeRepository _repository;

        public UpdateEmployeeStatusCommandHandler(IEmployeeRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Unit> Handle(UpdateEmployeeStatusCommand request, CancellationToken cancellationToken)
        {
            var employee = await _repository.GetEmployeeById(request.EmployeeId);
            if (employee == null)
            {
                throw new NotFoundException(nameof(Employee), request.EmployeeId);
            }

            employee.Status = request.Status;
            employee.UpdatedDate = DateTime.UtcNow;

            await _repository.UpdateEmployeeStatus(employee, cancellationToken);

            return Unit.Value;
        }
    }
}
