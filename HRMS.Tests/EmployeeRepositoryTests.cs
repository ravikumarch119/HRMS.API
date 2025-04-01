using HRMS.Application.Employees.Interfaces;
using HRMS.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace HRMS.Tests
{
    public class EmployeeRepositoryTests
    {
        private readonly Mock<IEmployeeRepository> _mockRepo;

        public EmployeeRepositoryTests()
        {
            _mockRepo = new Mock<IEmployeeRepository>();
        }

        [Fact]
        public async Task GetAllEmployees_ReturnsList()
        {
            _mockRepo.Setup(repo => repo.GetAllEmployees())
                     .ReturnsAsync(new List<Employee> { new Employee { Name = "John Doe" } });

            var result = await _mockRepo.Object.GetAllEmployees();
            Assert.Single(result);
        }

        [Fact]
        public async Task GetEmployeeById_ReturnsEmployee()
        {
            var employee = new Employee { EmployeeID = 1, Name = "John Doe" };
            _mockRepo.Setup(repo => repo.GetEmployeeById(1))
                     .ReturnsAsync(employee);

            var result = await _mockRepo.Object.GetEmployeeById(1);
            Assert.Equal(employee, result);
        }

        [Fact]
        public async Task AddEmployee_AddsEmployee()
        {
            var employee = new Employee { Name = "John Doe" };
            _mockRepo.Setup(repo => repo.AddEmployee(employee))
                     .Returns(Task.CompletedTask)
                     .Verifiable();

            await _mockRepo.Object.AddEmployee(employee);
            _mockRepo.Verify();
        }

        [Fact]
        public async Task UpdateEmployee_UpdatesEmployee()
        {
            var employee = new Employee { EmployeeID = 1, Name = "John Doe" };
            _mockRepo.Setup(repo => repo.UpdateEmployee(employee))
                     .Returns(Task.CompletedTask)
                     .Verifiable();

            await _mockRepo.Object.UpdateEmployee(employee);
            _mockRepo.Verify();
        }

        [Fact]
        public async Task DeleteEmployee_DeletesEmployee()
        {
            _mockRepo.Setup(repo => repo.DeleteEmployee(1))
                     .Returns(Task.CompletedTask)
                     .Verifiable();

            await _mockRepo.Object.DeleteEmployee(1);
            _mockRepo.Verify();
        }

        [Fact]
        public async Task UpdateEmployeeStatus_UpdatesStatus()
        {
            var employee = new Employee { EmployeeID = 1, Status = "Active" };
            var cancellationToken = new System.Threading.CancellationToken();
            _mockRepo.Setup(repo => repo.UpdateEmployeeStatus(employee, cancellationToken))
                     .Returns(Task.CompletedTask)
                     .Verifiable();

            await _mockRepo.Object.UpdateEmployeeStatus(employee, cancellationToken);
            _mockRepo.Verify();
        }
    }
}