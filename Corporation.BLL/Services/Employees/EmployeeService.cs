using Corporation.BLL.Models.Employees;
using Corporation.DAL.Models.Employees;
using Corporation.DAL.Persistence.Repositories.Employees;
using Microsoft.EntityFrameworkCore;

namespace Corporation.BLL.Services.Employees
{
    public class EmployeeService(IEmployeeRepository employeeRepository) : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;

        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            return _employeeRepository
                .GetAllAsIQueryable()
                .Where(E => !E.IsDeleted)
                .Include(E => E.Department)
                .Select(E => new EmployeeDto
                {
                    Id = E.Id,
                    Name = E.Name,
                    Age = E.Age,
                    IsActive = E.IsActive,
                    Salary = E.Salary,
                    Email = E.Email,
                    Gender = E.Gender.ToString(),
                    EmployeeType = E.EmployeeType.ToString(),
                    Department = E.Department.Name,
                });
        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _employeeRepository.Get(id);
            if (employee is not null)
                return new EmployeeDetailsDto()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Address = employee.Address,
                    IsActive = employee.IsActive,
                    Salary = employee.Salary,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    HiringDate = employee.HiringDate,
                    Gender = employee.Gender,
                    EmployeeType = employee.EmployeeType,
                    Department = employee.Department?.Name,
                };

            return null;
        }

        public int CreateEmployee(CreatedEmployeeDto employee)
        {
            var created = new Employee()
            {
                Name = employee.Name,
                Age = employee.Age,
                Email = employee.Email,
                Salary = employee.Salary,
                Address = employee.Address,
                IsActive = employee.IsActive,
                PhoneNumber = employee.PhoneNumber,
                HiringDate = employee.HiringDate,
                Gender = employee.Gender,
                EmployeeType = employee.EmployeeType,
                DepartmentId = employee.DepartmentId,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
            };

            return _employeeRepository.Add(created);
        }

        public int UpdateEmployee(UpdatedEmployeeDto employee)
        {
            var Updated = new Employee()
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Email = employee.Email,
                Salary = employee.Salary,
                Address = employee.Address,
                IsActive = employee.IsActive,
                PhoneNumber = employee.PhoneNumber,
                HiringDate = employee.HiringDate,
                Gender = employee.Gender,
                EmployeeType = employee.EmployeeType,
                DepartmentId = employee.DepartmentId,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
            };

            return _employeeRepository.Update(Updated);
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepository.Get(id);
            if (employee is { })
                _employeeRepository.Delete(employee);

            return false;
        }
    }
}
