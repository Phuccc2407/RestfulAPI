using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RestfulAPI.Repos.Models;

public partial class Album
{
    [Key]
    public string AlbumId { get; set; }

    public string ArtistId { get; set; }

    [StringLength(300)]
    public string Title { get; set; } = null!;

    public DateOnly? ReleaseDate { get; set; }

    [StringLength(2000)]
    public string? CoverUrl { get; set; }

    public int? LabelId { get; set; }

    public DateTime? CreatedAt { get; set; }

    [ForeignKey("ArtistId")]
    [InverseProperty("Albums")]
    public virtual Artist Artist { get; set; } = null!;

    [ForeignKey("LabelId")]
    [InverseProperty("Albums")]
    public virtual Label? Label { get; set; }

    [InverseProperty("Album")]
    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
}
