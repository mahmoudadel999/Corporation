using Corporation.DAL.Models.Department;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corporation.DAL.Persistence.Repositories.Departments
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll(bool WithAsNoTracking = true);
        IQueryable<Department> GetAllAsIQueryable();
        Department? Get(int id);
        int Add(Department entity);
        int Update(Department entity);
        int Delete(Department entity);
    }
}
