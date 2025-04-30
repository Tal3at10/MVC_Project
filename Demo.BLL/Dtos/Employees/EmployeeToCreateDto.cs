using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Dtos.Employees
{
    public class EmployeeToCreateDto
    {
        [Required]
        [MaxLength(50,ErrorMessage ="maximum Lengh is 50")]
        [MinLength(3, ErrorMessage = "minimum Lengh is 3")]
        public string Name { get; set; } = null!;

        [Range(22,30)]
        public int? Age { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9\s,.'-]{3,100}$", ErrorMessage = "Address is not valid")]
        public string? Address { get; set; }
        public decimal Salary { get; set; }

        [DisplayName("IS active")]
        public bool IsActive { get; set; }
        [DisplayName("Phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        public DateOnly HiringDate { get; set; }
        public string Gander { get; set; }
        public string EmployeeType { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;  // Set default value for CreatedOn
        public DateTime? LastModifiedOn { get; set; }
        public int? LastModifiedBy { get; set; }
        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }
    }
}
