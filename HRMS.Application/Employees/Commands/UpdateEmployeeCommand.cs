using HRMS.Application.Common.Exceptions;
using HRMS.Application.Employees.Interfaces;
using HRMS.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Employees.Commands
{
    public class UpdateEmployeeCommand : IRequest<Unit>
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public int DepartmentID { get; set; }
    }
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Unit>
    {
        private readonly IEmployeeRepository _repository;

        public UpdateEmployeeCommandHandler(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _repository.GetEmployeeById(request.EmployeeId);
            if (employee == null)
            {
                throw new NotFoundException(nameof(Employee), request.EmployeeId);
            }

            employee.Name = request.Name;
            employee.BirthDate = request.BirthDate;
            employee.Phone = request.Phone;
            employee.Email = request.Email;
            employee.Gender = request.Gender;
            employee.Address = request.Address;
            employee.Status = request.Status;
            employee.DepartmentID = request.DepartmentID;
            employee.UpdatedDate = DateTime.UtcNow;

            await _repository.UpdateEmployee(employee);

            return Unit.Value;
        }
    }
}
