namespace RestfulAPI.Repos.Models
{
    public class TrackCategory
    {
        public string TrackId { get; set; }
        public Track Track { get; set; } = null!;

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}
