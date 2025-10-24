using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RestfulAPI.Repos.Models;

public partial class PlaylistItem
{
    [Key]
    public long PlaylistItemId { get; set; }

    public string PlaylistId { get; set; }

    public string TrackId { get; set; }

    public int Position { get; set; }

    public DateTime? AddedAt { get; set; }

    [ForeignKey("PlaylistId")]
    [InverseProperty("PlaylistItems")]
    public virtual Playlist Playlist { get; set; } = null!;

    [ForeignKey("TrackId")]
    [InverseProperty("PlaylistItems")]
    public virtual Track Track { get; set; } = null!;
}
