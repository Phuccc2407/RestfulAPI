using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RestfulAPI.Repos.Models;

[Index("LabelName", Name = "UQ__Labels__6AD93A5464275C53", IsUnique = true)]
public partial class Label
{
    [Key]
    public int LabelId { get; set; }

    [StringLength(200)]
    public string LabelName { get; set; } = null!;

    [InverseProperty("Label")]
    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();
}
