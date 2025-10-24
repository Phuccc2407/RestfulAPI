using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestfulAPI.Repos.Models
{
    public class UserRole : IdentityUserRole<string>
    {
        public DateTime? AssignedAt { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; } = null!;

        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;
    }
}
