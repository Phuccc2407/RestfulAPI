using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RestfulAPI.Repos.Models;

[Table("StreamHistory")]
public partial class StreamHistory
{
    [Key]
    public long StreamId { get; set; }

    public string TrackId { get; set; }

    public string? UserId { get; set; }

    public DateTime? StartedAt { get; set; }

    public int? DurationPlayedSeconds { get; set; }

    [StringLength(500)]
    public string? DeviceInfo { get; set; }

    [StringLength(50)]
    public string? IpAddress { get; set; }

    [ForeignKey("TrackId")]
    [InverseProperty("StreamHistories")]
    public virtual Track Track { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("StreamHistories")]
    public virtual User? User { get; set; }
}
