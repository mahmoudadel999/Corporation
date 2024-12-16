using Corporation.DAL.Models.Department;
using Corporation.DAL.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corporation.DAL.Persistence.Repositories.Departments
{
    public class DepartmentRepository(AppDbContext dbContext) : IDepartmentRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public IEnumerable<Department> GetAll(bool WithAsNoTracking = true)
        {
            if (WithAsNoTracking)
            {
                return _dbContext.Departments.AsNoTracking().ToList();
            }
            return _dbContext.Departments.ToList();
        }

        public IQueryable<Department> GetAllAsIQueryable()
        {
            return _dbContext.Departments;
        }

        public Department? Get(int id)
        {
            return _dbContext.Departments.Find(id);
        }

        public int Add(Department entity)
        {
            _dbContext.Departments.Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Update(Department entity)
        {
            _dbContext.Departments.Update(entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(Department entity)
        {
            _dbContext.Departments.Remove(entity);
            return _dbContext.SaveChanges();
        }
    }
}
