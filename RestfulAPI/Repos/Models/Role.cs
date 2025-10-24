using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestfulAPI.Repos.Models
{
    public class Role : IdentityRole
    {
        [InverseProperty("Role")]
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
