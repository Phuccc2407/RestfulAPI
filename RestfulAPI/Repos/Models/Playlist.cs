using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RestfulAPI.Repos.Models;

public partial class Playlist
{
    [Key]
    public string PlaylistId { get; set; }

    public string OwnerUserId { get; set; }

    [StringLength(300)]
    public string Title { get; set; } = null!;

    public bool? IsPublic { get; set; }

    public DateTime? CreatedAt { get; set; }

    [ForeignKey("OwnerUserId")]
    [InverseProperty("Playlists")]
    public virtual User OwnerUser { get; set; } = null!;

    [InverseProperty("Playlist")]
    public virtual ICollection<PlaylistItem> PlaylistItems { get; set; } = new List<PlaylistItem>();
}
