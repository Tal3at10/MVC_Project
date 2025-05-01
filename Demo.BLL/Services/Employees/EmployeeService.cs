using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.Dtos.Employees;
using Demo.DAL.Entities.Common.Enums;
using Demo.DAL.Entities.Employees;
using Demo.DAL.Presistance.Reposateries.Employees;
using Demo.DAL.Presistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Demo.BLL.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository) // Ask Clr To Create Instance
        {
            _employeeRepository = employeeRepository;
        }
        public int CreateEmployee(EmployeeToCreateDto EmployeeDto)
        {
            Employee employee = new Employee()
            {
                Name = EmployeeDto.Name,
                Age = EmployeeDto.Age,
                Address = EmployeeDto.Address,
                IsActive = EmployeeDto.IsActive,
                PhoneNumber = EmployeeDto.PhoneNumber,
                HiringDate = EmployeeDto.HiringDate,
                Gander = Enum.Parse<Gander>(EmployeeDto.Gander.ToString()),
                EmployeeType = Enum.Parse<EmployeeType>(EmployeeDto.EmployeeType.ToString()),
                Email = EmployeeDto.Email,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
                DepartmentId = EmployeeDto.DepartmentId,
            };
            return _employeeRepository.AddT(employee); // Nuber of affected rows

        }

        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepository.GetById(id);

            if (employee != null)
                return _employeeRepository.DeleateT(employee) > 0; // Rows affected > 0 rteurn true , else return false

            return false;
        }

        public IEnumerable<EmployeeToReturnDto> GetAllEmployees()
        {
            var employees = _employeeRepository.GetAllQuarable()
                .Include(e => e.Department)
                .Where(e => !e.IsDeleted)
                .Select(employee => new EmployeeToReturnDto
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Email = employee.Email,
                    IsActive = employee.IsActive,
                    Salary = employee.Salary,
                    EmployeeType = employee.EmployeeType.ToString(),
                    Gander = employee.Gander.ToString(),
                    Department = employee.Department != null ? employee.Department.Name : null // use Eager Loading
                })
                .ToList();

            return employees;
        }

        public EmployeeDetailsToReaturnDto? GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetById(id);

            if (employee != null)
            {
                return new EmployeeDetailsToReaturnDto()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Email = employee.Email,
                    IsActive = employee.IsActive,
                    Address = employee.Address,
                    HiringDate = employee.HiringDate,
                    Salary = employee.Salary,
                    PhoneNumber = employee.PhoneNumber,
                    EmployeeType = employee.EmployeeType.ToString(),
                    Gander = employee.Gander.ToString(),
                    LastModifiedBy = employee.LastModifiedBy,
                    LastModifiedOn = employee.LastModifiedOn,
                    CreatedBy = employee.CreatedBy,
                    CreatedOn = employee.CreatedOn,
                    Department = employee.Department.Name, // Lazy Loading
                };
            }

            return null;
        }


        public int UpdateEmployee(EmployeeToUpdateDto EmployeeDto)
        {
            Employee employee = new Employee
            {
                Id = EmployeeDto.Id,
                Name = EmployeeDto.Name,
                Age = EmployeeDto.Age,
                Email = EmployeeDto.Email,
                IsActive = EmployeeDto.IsActive,
                Address = EmployeeDto.Address,
                HiringDate = EmployeeDto.HiringDate,
                Salary = EmployeeDto.Salary,
                PhoneNumber = EmployeeDto.PhoneNumber,
                Gander = Enum.Parse<Gander>(EmployeeDto.Gander.ToString()),
                EmployeeType = Enum.Parse<EmployeeType>(EmployeeDto.EmployeeType.ToString()),
                LastModifiedBy = EmployeeDto.LastModifiedBy,
                LastModifiedOn = EmployeeDto.LastModifiedOn,
                CreatedBy = EmployeeDto.CreatedBy,
                CreatedOn = EmployeeDto.CreatedOn,
                DepartmentId = EmployeeDto.DepartmentId,

            };

            return _employeeRepository.UpadteT(employee);
        }
    }
}