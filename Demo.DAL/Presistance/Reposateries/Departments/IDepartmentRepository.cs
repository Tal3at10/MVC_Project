using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Entities.Departments;

namespace Demo.DAL.Presistance.Reposateries.Departments
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll(bool AsNoTracking = true);
        IQueryable<Department> GetAllQuarable();
        Department? GetById(int id);
        int AddDepartment(Department entity);
        int UpadteDepartment(Department entity);
        int DeleateDepartment(Department entity);

        
    }
}
