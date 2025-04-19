using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Entities.Departments;
using Demo.DAL.Presistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Demo.DAL.Presistance.Reposateries.Departments
{
    // Database => Repository => Services => Comtroller.
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public DepartmentRepository(ApplicationDbContext dbContext) // ask Clr to create instance
        { 
            _dbContext = dbContext;
        }
        public int AddDepartment(Department entity)
        {
            _dbContext.Departments.Add(entity); // Saved Locally
            return _dbContext.SaveChanges();
        }

        public int DeleateDepartment(Department entity)
        {
            _dbContext.Departments.Remove(entity);
            return _dbContext.SaveChanges(); 
        }

        public int UpadteDepartment(Department entity)
        {
            _dbContext.Departments.Update(entity); // Modified
            return _dbContext.SaveChanges(); //unchanged
        }

        public IEnumerable<Department> GetAll(bool AsNoTracking = true)
        {
            if (AsNoTracking)
            {
                return _dbContext.Departments.AsNoTracking().ToList(); //ditached
            }
            return _dbContext.Departments.ToList(); //unchanged

        }

        public Department? GetById(int id)
        {
            //return _dbContext.Departments.Local.FirstOrDefault(d => d.Id == id);

            return _dbContext.Departments.Find(id); // search Locally in case found return , else request DB.
        }

        public IQueryable<Department> GetAllQuarable()
        {
            return _dbContext.Departments;
        }

    }
}
