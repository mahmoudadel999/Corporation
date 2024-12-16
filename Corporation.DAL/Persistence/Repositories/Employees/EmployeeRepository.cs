using Corporation.DAL.Models.Employees;
using Corporation.DAL.Persistence.Data;
using Corporation.DAL.Persistence.Repositories._IGenericBase;
using Microsoft.EntityFrameworkCore;

namespace Corporation.DAL.Persistence.Repositories.Employees
{
    public class EmployeeRepository(AppDbContext dbContext)
                : GenericRepositoryBase<Employee>(dbContext), IEmployeeRepository
    {
    }
}
