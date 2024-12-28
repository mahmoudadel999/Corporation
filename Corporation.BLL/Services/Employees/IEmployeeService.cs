using Corporation.BLL.Models.Employees;

namespace Corporation.BLL.Services.Employees
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(string search);
        Task<EmployeeDetailsDto?> GetEmployeeByIdAsync(int id);
        Task<int> CreateEmployeeAsync(CreatedEmployeeDto employee);
        Task<int> UpdateEmployeeAsync(UpdatedEmployeeDto employee);
        Task<bool> DeleteEmployeeAsync(int id);
    }
}
