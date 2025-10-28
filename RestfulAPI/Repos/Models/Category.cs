using System.ComponentModel.DataAnnotations;

namespace RestfulAPI.Repos.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;

        public virtual ICollection<TrackCategory> TrackCategories { get; set; } = new List<TrackCategory>();
    }
}
