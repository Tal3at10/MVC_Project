using System.Linq;
using Demo.BLL.Dtos;
using Demo.BLL.Dtos.Departments;
using Demo.DAL.Entities.Departments;

namespace Demo.BLL.Services.Departments
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentToReturnDto> GetAllDepartments();
        DepartmentDetailsToReaturnDto? GetDepartmentById(int id);
        int CreateDepartment(DepartmentToCreateDto department);
        int UpdateDepartment(DepartmentToUpdateDto department);
        bool DeleteDepartment(int id);
    }
}