using Corporation.BLL.Models.Departments;
using Corporation.DAL.Models.Departments;
using Corporation.DAL.Persistence.Repositories.Departments;
using Corporation.DAL.Persistence.UintOfWork;
using Microsoft.EntityFrameworkCore;

namespace Corporation.BLL.Services.Departments
{
    public class DepartmentService(IUnitOfWork unitOfWork) : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
            var departments = _unitOfWork.DepartmentRepository
                .GetAllAsIQueryable()
                .Select(department => new DepartmentDto
                {
                    Id = department.Id,
                    Name = department.Name,
                    Code = department.Code,
                    CreationDate = department.CreationDate,
                }).AsNoTracking().ToListAsync();

            return await departments;
        }

        public async Task<DepartmentDetailsDto?> GetDepartmentByIdAsync(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id);

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

        public async Task<int> CreateDepartmentAsync(CreatedDepartmentDto department)
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

            _unitOfWork.DepartmentRepository.Add(CreatedDepartment);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> UpdateDepartmentAsync(UpdatedDepartmentDto department)
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

            _unitOfWork.DepartmentRepository.Update(UpdatedDepartment);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var departmentRepos = _unitOfWork.DepartmentRepository;
            var department = await departmentRepos.GetAsync(id);
            if (department is not null)
                departmentRepos.Delete(department);

            return await _unitOfWork.CompleteAsync() > 0;
        }
    }
}
