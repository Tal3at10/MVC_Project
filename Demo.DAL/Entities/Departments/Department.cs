using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Entities.Employees;

namespace Demo.DAL.Entities.Departments
{
    public class Department : ModelBase
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Code { get; set; } = null!;
        public DateTime CreationDate { get; set; }

        // Navigational Property [Many]
       public IEnumerable<Employee> Employees { get; set;} = new HashSet<Employee>();
    }
}
