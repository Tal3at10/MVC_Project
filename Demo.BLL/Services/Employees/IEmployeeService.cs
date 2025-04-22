using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.Dtos.Employees;
using Demo.BLL.Dtos;

namespace Demo.BLL.Services.Employees
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeToReturnDto> GetAllEmployees();
        EmployeeDetailsToReaturnDto? GetEmployeeById(int id);
        int CreateEmployee(EmployeeToCreateDto department);
        int UpdateEmployee(EmployeeToUpdateDto department);
        bool DeleteEmployee(int id);
    }

}
