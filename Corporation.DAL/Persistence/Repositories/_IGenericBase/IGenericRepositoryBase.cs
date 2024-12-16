using Corporation.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corporation.DAL.Persistence.Repositories._GenericBase
{
    public interface IGenericRepositoryBase<T> where T : ModelBase
    {
        T? Get(int id);
        IEnumerable<T> GetAll(bool WithAsNoTracking = true);
        IQueryable<T> GetAllAsIQueryable();
        int Add(T entity);
        int Update(T entity);
        int Delete(T entity);
    }
}
