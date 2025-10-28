namespace RestfulAPI.Repos.Models
{
    public class TrackGenre
    {
        public string TrackId { get; set; }
        public Track Track { get; set; } = null!;

        public int GenreId { get; set; }
        public Genre Genre { get; set; } = null!;
    }
}
