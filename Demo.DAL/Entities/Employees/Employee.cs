using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Entities.Common.Enums;

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
        public DateOnly HiringDate { get; set; }
        public Gander Gander { get; set; }
        public EmployeeType EmployeeType { get; set; }

    }
}
