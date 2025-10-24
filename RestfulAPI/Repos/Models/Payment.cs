using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RestfulAPI.Repos.Models;

public partial class Payment
{
    [Key]
    public long PaymentId { get; set; }

    public string UserId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Amount { get; set; }

    [StringLength(10)]
    public string Currency { get; set; } = null!;

    public DateTime? PaymentDate { get; set; }

    [StringLength(100)]
    public string? Provider { get; set; }

    [StringLength(200)]
    public string? Reference { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Payments")]
    public virtual User User { get; set; } = null!;
}
