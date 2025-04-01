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
    public class AddEmployeeCommand : IRequest<int>
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }

        public int Department { get; set; }

        public string City { get; set; }
    }

    public class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommand, int>
    {
        private readonly IEmployeeRepository _repository;

        public AddEmployeeCommandHandler(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee
            {
                Name = request.Name,
                BirthDate = request.BirthDate,
                Phone = request.Phone,
                Email = request.Email,
                Gender = request.Gender,
                Address = request.Address,
                Status = request.Status,
                DepartmentID = request.Department,
                InsertedDate = DateTime.Now,

            };

            await _repository.AddEmployee(employee);
            return employee.EmployeeID;
        }
    }
}
