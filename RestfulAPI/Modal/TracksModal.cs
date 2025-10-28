namespace RestfulAPI.Modal
{
    public class TracksModal
    {
        public string TrackId { get; set; }
        public string Title { get; set; }
        public List<ArtistModal> Artists { get; set; } = new List<ArtistModal>();
        public List<GenreModal> Genres { get; set; } = new List<GenreModal>();
        public List<CategoryModal> Categories { get; set; } = new List<CategoryModal>();
        public string? AlbumTitle { get; set; }
        public string FileUrl { get; set; }
        public string TrackImageUrl { get; set; }
        public int DurationSeconds { get; set; }
        public int? Bitrate { get; set; }
        public bool? Explicit { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
