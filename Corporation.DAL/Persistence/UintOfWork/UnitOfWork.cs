using Corporation.DAL.Persistence.Data;
using Corporation.DAL.Persistence.Repositories.Departments;
using Corporation.DAL.Persistence.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corporation.DAL.Persistence.UintOfWork
{
    public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
    {
        private readonly AppDbContext _dbContext = dbContext;

        public IEmployeeRepository EmployeeRepository => new EmployeeRepository(_dbContext);
        public IDepartmentRepository DepartmentRepository => new DepartmentRepository(_dbContext);

        public async Task<int> CompleteAsync() => await _dbContext.SaveChangesAsync();


        public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();

    }
}
