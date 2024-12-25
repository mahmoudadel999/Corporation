using Corporation.DAL.Models.Departments;
using Corporation.DAL.Models.Employees;
using Corporation.DAL.Persistence.Repositories.Departments;
using Corporation.DAL.Persistence.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corporation.DAL.Persistence.UintOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IEmployeeRepository EmployeeRepository { get; }
        public IDepartmentRepository DepartmentRepository { get; }

        int Complete();
    }
}
