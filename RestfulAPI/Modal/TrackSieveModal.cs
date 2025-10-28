using Sieve.Attributes;

namespace RestfulAPI.Modal
{
    public class TrackSieveModal
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public string Title { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public int DurationSeconds { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string? AlbumTitle { get; set; }

        // Nếu muốn lọc theo artist, genre hoặc category
        [Sieve(CanFilter = true)]
        public string ArtistName { get; set; }

        [Sieve(CanFilter = true)]
        public string GenreName { get; set; }
        [Sieve(CanFilter = true)]
        public string CategoryName { get; set; }
    }
}
