using Corporation.BLL.Models.Departments;
using Corporation.DAL.Models.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corporation.BLL.Services.Departments
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();
        Task<DepartmentDetailsDto?> GetDepartmentByIdAsync(int id);
        Task<int> CreateDepartmentAsync(CreatedDepartmentDto department);
        Task<int> UpdateDepartmentAsync(UpdatedDepartmentDto department);
        Task<bool> DeleteDepartmentAsync(int id);
    }
}
