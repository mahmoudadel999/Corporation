using Corporation.DAL.Models.Departments;
using Corporation.DAL.Persistence.Data;
using Corporation.DAL.Persistence.Repositories._IGenericBase;

namespace Corporation.DAL.Persistence.Repositories.Departments
{
    public class DepartmentRepository(AppDbContext dbContext) :
        GenericRepositoryBase<Department>(dbContext), IDepartmentRepository
    {
    }
}
