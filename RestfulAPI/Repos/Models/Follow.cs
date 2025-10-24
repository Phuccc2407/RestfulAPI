using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RestfulAPI.Repos.Models;

public partial class Follow
{
    [Key]
    public long FollowId { get; set; }

    public string FollowerId { get; set; }

    public string? FolloweeArtistId { get; set; }

    public string? FolloweeUserId { get; set; }

    public DateTime? FollowedAt { get; set; }

    [ForeignKey("FolloweeArtistId")]
    [InverseProperty("Follows")]
    public virtual Artist? FolloweeArtist { get; set; }

    [ForeignKey("FolloweeUserId")]
    [InverseProperty("FollowFolloweeUsers")]
    public virtual User? FolloweeUser { get; set; }

    [ForeignKey("FollowerId")]
    [InverseProperty("FollowFollowers")]
    public virtual User Follower { get; set; } = null!;
}
