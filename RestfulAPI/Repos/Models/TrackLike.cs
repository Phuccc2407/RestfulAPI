using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RestfulAPI.Repos.Models;

[PrimaryKey("TrackId", "UserId")]
public partial class TrackLike
{
    [Key]
    public string TrackId { get; set; }

    [Key]
    public string UserId { get; set; }

    public DateTime? LikedAt { get; set; }

    [ForeignKey("TrackId")]
    [InverseProperty("TrackLikes")]
    public virtual Track Track { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("TrackLikes")]
    public virtual User User { get; set; } = null!;
}
