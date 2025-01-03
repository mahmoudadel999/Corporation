﻿using Corporation.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corporation.DAL.Persistence.Repositories._GenericBase
{
    public interface IGenericRepositoryBase<T> where T : ModelBase
    {
        Task<T?> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(bool WithAsNoTracking = true);
        IQueryable<T> GetAllAsIQueryable();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
