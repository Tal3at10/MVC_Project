using System;
using System.Collections.Generic;
using System.Linq;
using Demo.BLL.Dtos;
using Demo.BLL.Dtos.Departments;
using Demo.BLL.Services.Departments;
using Demo.DAL.Entities.Departments;
using Demo.DAL.Presistance.Reposateries.Departments;
using Microsoft.EntityFrameworkCore;

namespace Demo.BLL.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public IEnumerable<DepartmentToReturnDto> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAllQuarable()
                .Where(d => d.IsDeleted == false)
                .AsNoTracking()
                .Select(d => new DepartmentToReturnDto
                {
                    Description = d.Description,
                    CreationDate = d.CreationDate,
                    Code = d.Code,
                    Id = d.Id,
                    Name = d.Name,
                })
                .ToList();

            return departments;
        }


        public DepartmentDetailsToReaturnDto? GetDepartmentById(int id)
        {
            var department = _departmentRepository.GetById(id);
            if (department != null)
            {
                return new DepartmentDetailsToReaturnDto
                {
                    Id = department.Id,
                    Name = department.Name,
                    Code = department.Code,
                    CreationDate = department.CreationDate,
                    CreatedBy = department.CreatedBy,
                    CreatedOn = department.CreatedOn,
                    LastModifiedOn = department.LastModifiedOn,
                    Description = department.Description,
                    IsDeleted = department.IsDeleted,
                };

            }
            return null;
        }

        public int CreateDepartment(DepartmentToCreateDto department)
        {
            var newDepartment = new Department
            {
                Name = department.Name,
                Description = department.Description,
                Code = department.Code,
                CreationDate = department.CreationDate, // assuming you want the current UTC time
                CreatedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
                LastModifiedBy = 1,

            };

            return _departmentRepository.AddT(newDepartment);
        }

        public int UpdateDepartment(DepartmentToUpdateDto department)
        {
            var updateDepartment = new Department
            {
                Name = department.Name,
                Description = department.Description,
                Code = department.Code,
                CreationDate = department.CreationDate, // assuming you want the current UTC time
                CreatedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
                LastModifiedBy = 1,

            };

            return _departmentRepository.AddT(updateDepartment);
        }

        public bool DeleteDepartment(int id)
        {
            var department = _departmentRepository.GetById(id);

            if ((department is not null))
            {
                return _departmentRepository.DeleateT(department) > 0;
            }
            return false;

        }

       
    }
}