using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Entities.Employees;
using Demo.DAL.Presistance.Reposateries.Generics;
using Demo.DAL.Presistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Demo.DAL.Presistance.Reposateries.Employees
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext) 
        {

        }
      
    }
}
