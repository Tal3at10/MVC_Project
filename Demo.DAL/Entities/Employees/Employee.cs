using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Entities.Common.Enums;
using Demo.DAL.Entities.Departments;

namespace Demo.DAL.Entities.Employees
{
    public class Employee : ModelBase
    {
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string? PhoneNumber  { get; set; }
        public string? Email { get; set; }
        public DateOnly HiringDate { get; set; }
        public Gander Gander { get; set; }
        public EmployeeType EmployeeType { get; set; }

        // Navigational Property[Not Be Loaded By Deafult] [One]
        public virtual Department? Department { get; set; }
        public int? DepartmentId { get; set; }

    }
}
