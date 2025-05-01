using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Entities.Common.Enums;

namespace Demo.BLL.Dtos.Employees
{
    public class EmployeeDetailsToReaturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateOnly HiringDate { get; set; }
        public string Gander { get; set; }
        public string EmployeeType { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;  // Set default value for CreatedOn
        public DateTime? LastModifiedOn { get; set; }
        public int? LastModifiedBy { get; set; }

        public string? Department { get; set; }
    }
}
