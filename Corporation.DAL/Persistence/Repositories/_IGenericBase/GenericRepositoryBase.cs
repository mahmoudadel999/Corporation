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

        public async Task<IEnumerable<T>> GetAllAsync(bool WithAsNoTracking = true)
        {
            if (WithAsNoTracking)
                await _dbContext.Set<T>().Where(X => !X.IsDeleted).AsNoTracking().ToListAsync();

            return await _dbContext.Set<T>().ToListAsync();
        }

        public IQueryable<T> GetAllAsIQueryable()
        {
            return _dbContext.Set<T>();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await _dbContext.FindAsync<T>(id);
        }

        public void Add(T entity) => _dbContext.Set<T>().Add(entity);

        public void Update(T entity) => _dbContext.Set<T>().Update(entity);

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            _dbContext.Set<T>().Remove(entity);
        }
    }
}
