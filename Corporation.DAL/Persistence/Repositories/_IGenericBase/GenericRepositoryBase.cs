using Corporation.DAL.Models;
using Corporation.DAL.Models.Employees;
using Corporation.DAL.Persistence.Data;
using Corporation.DAL.Persistence.Repositories._GenericBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corporation.DAL.Persistence.Repositories._IGenericBase
{
    public class GenericRepositoryBase<T>(AppDbContext dbContext) where T : ModelBase
    {
        private protected readonly AppDbContext _dbContext = dbContext;

        public IEnumerable<T> GetAll(bool WithAsNoTracking = true)
        {
            if (WithAsNoTracking)
                _dbContext.Set<T>().Where(E => E.Id > 10).AsNoTracking().ToList();

            return _dbContext.Set<T>().ToList();
        }

        public IQueryable<T> GetAllAsQueryable()
        {
            return _dbContext.Set<T>();
        }

        public T? Get(int id)
        {
            return _dbContext.Find<T>(id);
        }

        public int Add(T employee)
        {
            _dbContext.Set<T>().Add(employee);
            return _dbContext.SaveChanges();
        }

        public int Update(T employee)
        {
            _dbContext.Set<T>().Update(employee);
            return _dbContext.SaveChanges();
        }

        public int Delete(T employee)
        {
            _dbContext.Set<T>().Remove(employee);
            return _dbContext.SaveChanges();
        }
    }
}
