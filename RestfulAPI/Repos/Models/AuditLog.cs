using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RestfulAPI.Repos.Models;

public partial class AuditLog
{
    [Key]
    public long AuditId { get; set; }

    [StringLength(100)]
    public string? Entity { get; set; }

    [StringLength(100)]
    public string? EntityId { get; set; }

    [StringLength(50)]
    public string? Action { get; set; }

    public string? PerformedBy { get; set; }

    public DateTime? PerformedAt { get; set; }

    public string? Details { get; set; }

    [ForeignKey("PerformedBy")]
    [InverseProperty("AuditLogs")]
    public virtual User? PerformedByNavigation { get; set; }
}
