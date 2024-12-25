using Corporation.BLL.Models.Employees;
using Corporation.DAL.Models.Employees;
using Corporation.DAL.Persistence.Repositories.Employees;
using Corporation.DAL.Persistence.UintOfWork;
using Microsoft.EntityFrameworkCore;

namespace Corporation.BLL.Services.Employees
{
    public class EmployeeService(IUnitOfWork unitOfWork) : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public IEnumerable<EmployeeDto> GetAllEmployees(string search)
        {
            return _unitOfWork.EmployeeRepository
                .GetAllAsIQueryable()
                .Where(E => !E.IsDeleted && (string.IsNullOrEmpty(search) || E.Name.ToLower().Contains(search.ToLower())))
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
                    Department = E.Department!.Name,
                });
        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.Get(id);
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

            _unitOfWork.EmployeeRepository.Add(created);
            return _unitOfWork.Complete();
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

            _unitOfWork.EmployeeRepository.Update(Updated);
            return _unitOfWork.Complete();
        }

        public bool DeleteEmployee(int id)
        {
            var employeeRepos = _unitOfWork.EmployeeRepository;
            var employee = employeeRepos.Get(id);
            if (employee is { })
                _unitOfWork.EmployeeRepository.Delete(employee);

            return false;
        }
    }
}
