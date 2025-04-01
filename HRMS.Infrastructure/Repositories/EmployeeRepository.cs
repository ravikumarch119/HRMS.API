using HRMS.Application.Employees.Interfaces;
using HRMS.Domain;
using HRMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HRMSDbContext _context;

        public EmployeeRepository(HRMSDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees() =>
            await _context.Employees.ToListAsync();

        public async Task<Employee?> GetEmployeeById(int id) =>
            await _context.Employees.FindAsync(id);

        public async Task AddEmployee(Employee employee)
        {
            var existingEmployee = await _context.Employees
        .FirstOrDefaultAsync(e => e.Email == employee.Email);

            if (existingEmployee != null)
            {
                throw new InvalidOperationException("An employee with this email already exists.");
            }

            var departmentExists = await _context.Departments
                .AnyAsync(d => d.DepartmentID == employee.DepartmentID);

            if (!departmentExists)
            {
                throw new InvalidOperationException("The specified department does not exist.");
            }

            _context.Employees.Add(employee);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Handle concurrency exception
                throw new InvalidOperationException("A concurrency error occurred while saving the employee.", ex);
            }
        }

        public async Task UpdateEmployee(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }  

        public async Task UpdateEmployeeStatus(Employee employee, CancellationToken cancellationToken)
        {
            var existingEmployee = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeID == employee.EmployeeID, cancellationToken);

            if (existingEmployee == null)
            {
                throw new InvalidOperationException("Employee not found.");
            }

            existingEmployee.Status = employee.Status;
            existingEmployee.UpdatedDate = DateTime.UtcNow;

            _context.Employees.Update(existingEmployee);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
