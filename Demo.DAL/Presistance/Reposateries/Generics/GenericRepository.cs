using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Entities;
using Demo.DAL.Presistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Demo.DAL.Presistance.Reposateries.Generics
{
    public class GenericRepository<T> :IGenericRepository<T> where T : ModelBase
    {
        private readonly ApplicationDbContext _dbContext;
        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int AddT(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return _dbContext.SaveChanges();
        }

        public int DeleateT(T entity)
        {
            //_dbContext.Set<T>().Remove(entity);
            //return _dbContext.SaveChanges();

            entity.IsDeleted = true;
            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges();
        }

        public int UpadteT(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<T> GetAll(bool AsNoTracking = true)
        {
            if (AsNoTracking)
            {
                return _dbContext.Set<T>().Where(x=>x.IsDeleted==false).AsNoTracking().ToList();
            }
            return _dbContext.Set<T>().Where(x => x.IsDeleted == false).ToList();
        }

        public T? GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public IQueryable<T> GetAllQuarable()
        {
            return _dbContext.Set<T>();
        }
    }
}
