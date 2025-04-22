using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Entities.Departments;
using Demo.DAL.Presistance.Reposateries.Generics;

namespace Demo.DAL.Presistance.Reposateries.Departments
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
    }
}
