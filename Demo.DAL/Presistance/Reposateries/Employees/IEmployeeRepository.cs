using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Entities.Employees;
using Demo.DAL.Entities.Employees;
using Demo.DAL.Presistance.Reposateries.Generics;

namespace Demo.DAL.Presistance.Reposateries.Employees
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
       
    }
}
