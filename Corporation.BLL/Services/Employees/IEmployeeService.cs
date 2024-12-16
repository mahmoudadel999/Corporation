using Corporation.BLL.Models.Employees;
using Corporation.DAL.Models.Department;
using Corporation.DAL.Models.Employees;

namespace Corporation.BLL.Services.Employees
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetAllEmployees();
        EmployeeDetailsDto? GetEmployeeById(int id);
        int CreateEmployee(CreatedEmployeeDto employee);
        int UpdateEmployee(UpdatedEmployeeDto employee);
        bool DeleteEmployee(int id);
    }
}
