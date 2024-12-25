using Corporation.BLL.Models.Departments;
using Corporation.DAL.Models.Departments;
using Corporation.DAL.Persistence.Repositories.Departments;
using Microsoft.EntityFrameworkCore;

namespace Corporation.BLL.Services.Departments
{
    public class DepartmentService(IDepartmentRepository departmentRepository) : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository = departmentRepository;

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAllAsIQueryable().Select(department => new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                CreationDate = department.CreationDate,
            }).AsNoTracking().ToList();

            return departments;
        }

        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var department = _departmentRepository.Get(id);

            if (department is { })
            {
                return new DepartmentDetailsDto
                {
                    Id = department.Id,
                    Name = department.Name,
                    Code = department.Code,
                    Description = department.Description!,
                    CreationDate = department.CreationDate,
                    CreatedBy = department.CreatedBy,
                    CreatedOn = department.CreatedOn,
                    LastModifiedBy = department.LastModifiedBy,
                    LastModifiedOn = department.LastModifiedOn,

                };
            };

            return null;
        }

        public int CreateDepartment(CreatedDepartmentDto department)
        {
            var CreatedDepartment = new Department()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,
                // CreatedOn = DateTime.UtcNow,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
            };

            return _departmentRepository.Add(CreatedDepartment);
        }

        public int UpdateDepartment(UpdatedDepartmentDto department)
        {
            var UpdatedDepartment = new Department()
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
            };

            return _departmentRepository.Update(UpdatedDepartment);
        }

        public bool DeleteDepartment(int id)
        {
            var department = _departmentRepository.Get(id);
            if (department is not null)
                return _departmentRepository.Delete(department) > 0;

            return false;
        }
    }
}
