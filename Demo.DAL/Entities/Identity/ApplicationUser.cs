using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace NoteKeeperPro.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsAgree { get; set; } = false;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
