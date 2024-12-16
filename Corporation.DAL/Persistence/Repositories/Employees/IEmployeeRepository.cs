﻿using Corporation.DAL.Models.Employees;
using Corporation.DAL.Persistence.Repositories._GenericBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corporation.DAL.Persistence.Repositories.Employees
{
    public interface IEmployeeRepository : IGenericRepositoryBase<Employee>
    {
    }
}