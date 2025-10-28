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

    [StringLength(300)]
    public string Title { get; set; } = null!;

    public int DurationSeconds { get; set; }

    [StringLength(2000)]
    public string FileUrl { get; set; } = null!;

    [StringLength(2000)]
    public string TrackImageUrl { get; set; } = null!;

    public int? Bitrate { get; set; }

    public bool? Explicit { get; set; }

    public DateTime? CreatedAt { get; set; }

    [ForeignKey("AlbumId")]
    [InverseProperty("Tracks")]
    public virtual Album? Album { get; set; }

    public virtual ICollection<TrackArtist> TrackArtists { get; set; } = new List<TrackArtist>();

    public virtual ICollection<TrackGenre> TrackGenres { get; set; } = new List<TrackGenre>();
    public virtual ICollection<TrackCategory> TrackCategories { get; set; } = new List<TrackCategory>();

    [InverseProperty("Track")]
    public virtual ICollection<PlaylistItem> PlaylistItems { get; set; } = new List<PlaylistItem>();

    [InverseProperty("Track")]
    public virtual ICollection<StreamHistory> StreamHistories { get; set; } = new List<StreamHistory>();

    [InverseProperty("Track")]
    public virtual ICollection<TrackLike> TrackLikes { get; set; } = new List<TrackLike>();
}
