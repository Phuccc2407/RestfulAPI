using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RestfulAPI.Repos.Models;

public partial class Track
{
    [Key]
    public string TrackId { get; set; }

    public string? AlbumId { get; set; }

    public string ArtistId { get; set; }

    [StringLength(300)]
    public string Title { get; set; } = null!;

    public int DurationSeconds { get; set; }

    [StringLength(2000)]
    public string FileUrl { get; set; } = null!;

    public int? Bitrate { get; set; }

    public bool? Explicit { get; set; }

    public DateTime? CreatedAt { get; set; }

    [ForeignKey("AlbumId")]
    [InverseProperty("Tracks")]
    public virtual Album? Album { get; set; }

    [ForeignKey("ArtistId")]
    [InverseProperty("Tracks")]
    public virtual Artist Artist { get; set; } = null!;

    [InverseProperty("Track")]
    public virtual ICollection<PlaylistItem> PlaylistItems { get; set; } = new List<PlaylistItem>();

    [InverseProperty("Track")]
    public virtual ICollection<StreamHistory> StreamHistories { get; set; } = new List<StreamHistory>();

    [InverseProperty("Track")]
    public virtual ICollection<TrackLike> TrackLikes { get; set; } = new List<TrackLike>();

    [ForeignKey("TrackId")]
    [InverseProperty("Tracks")]
    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
}
