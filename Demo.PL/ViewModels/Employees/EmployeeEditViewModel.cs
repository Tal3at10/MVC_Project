using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Demo.DAL.Entities.Common.Enums;

namespace Demo.PL.ViewModels.Employees
{
    public class EmployeeEditViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

       public int? Age { get; set; }

        public string? Address { get; set; }
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }
     
        public string? PhoneNumber { get; set; }
        public DateOnly HiringDate { get; set; }

        public string? Email { get; set; }
        public string Gander { get; set; }
        public string EmployeeType { get; set; }
    }
}
