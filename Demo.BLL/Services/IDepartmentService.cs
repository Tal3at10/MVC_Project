using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.Dtos;
using Demo.BLL.Dtos.Departments;
using Demo.DAL.Entities.Departments;

namespace Demo.BLL.Services
{
    public interface IDepartmentService
    {
      IEnumerable<DepartmentToReturnDto> GetAllDepartments();
        DepartmentDetailsToReaturnDto? GetDepartmentById(int id);
        int CreateDepartment(DepartmentToCreateDto department);
        int UpdateDepartment(DepartmentToUpdateDto departmeent);
        bool DeleteDepartment(int id);
    }
}
