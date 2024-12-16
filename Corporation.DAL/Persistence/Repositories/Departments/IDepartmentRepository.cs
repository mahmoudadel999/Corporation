using Corporation.DAL.Models.Department;
using Corporation.DAL.Persistence.Repositories._GenericBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corporation.DAL.Persistence.Repositories.Departments
{
    public interface IDepartmentRepository : IGenericRepositoryBase<Department>
    {
    }
}
