using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Dtos.Departments
{
    public class DepartmentDetailsToReaturnDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Code { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public int Id { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public DateTime? LastModifiedOn { get; set; }
        public DateTime? LastModifiedBy { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
