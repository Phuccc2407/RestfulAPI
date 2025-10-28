namespace RestfulAPI.Repos.Models
{
    public class TrackArtist
    {
        public string TrackId { get; set; }
        public Track Track { get; set; } = null!;

        public string ArtistId { get; set; }
        public Artist Artist { get; set; } = null!;
    }
}
