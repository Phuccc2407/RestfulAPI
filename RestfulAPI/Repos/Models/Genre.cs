using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RestfulAPI.Repos.Models;

[Index("Name", Name = "UQ__Genres__737584F6D4D38D44", IsUnique = true)]
public partial class Genre
{
    [Key]
    public int GenreId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    public virtual ICollection<TrackGenre> TrackGenres { get; set; } = new List<TrackGenre>();
}
