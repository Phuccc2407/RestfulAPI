using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RestfulAPI.Repos.Models;

public partial class Artist
{
    [Key]
    public string ArtistId { get; set; }

    public string? UserId { get; set; }

    [StringLength(200)]
    public string ArtistName { get; set; } = null!;

    public string? Bio { get; set; }

    public DateTime? CreatedAt { get; set; }

    [InverseProperty("Artist")]
    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();

    [InverseProperty("FolloweeArtist")]
    public virtual ICollection<Follow> Follows { get; set; } = new List<Follow>();

    [InverseProperty("Artist")]
    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();

    [ForeignKey("UserId")]
    [InverseProperty("Artists")]
    public virtual User? User { get; set; }
}
