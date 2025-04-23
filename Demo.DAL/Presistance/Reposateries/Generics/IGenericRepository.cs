using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Entities;

namespace Demo.DAL.Presistance.Reposateries.Generics
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        IEnumerable<T> GetAll(bool AsNoTracking = true);
        IQueryable<T> GetAllQuarable();
        T? GetById(int id);
        int AddT(T entity);
        int UpadteT(T entity);
        int DeleateT(T entity);
    }
}
