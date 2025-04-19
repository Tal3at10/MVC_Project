using System;

namespace Demo.DAL.Entities
{
    public class ModelBase
    {
        public int Id { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;  // Set default value for CreatedOn

        public DateTime? LastModifiedOn { get; set; }
        public int? LastModifiedBy { get; set; }  // Assuming LastModifiedBy refers to a user ID (int)

        public bool IsDeleted { get; set; } = false;
    }
}
