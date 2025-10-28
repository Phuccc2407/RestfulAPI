using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RestfulAPI.Repos.Models;

[Table("RefreshToken")]
public partial class RefreshToken
{
    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string UserId { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? TokenId { get; set; }

    [Column("RefreshToken")]
    [Unicode(false)]
    public string? RefreshToken1 { get; set; }

    public DateTime ExpiryDate { get; set; }
}
