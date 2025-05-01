using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Entities.Common.Enums;

namespace Demo.BLL.Dtos.Employees
{
    public class EmployeeToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [DisplayName("IS active")]
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string Gander { get; set; }
        public string EmployeeType { get; set; }
        public string? Department { get; set; }

    }
}
