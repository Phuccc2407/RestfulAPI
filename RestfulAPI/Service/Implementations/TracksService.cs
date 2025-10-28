using AutoMapper;
using Azure;
using Microsoft.EntityFrameworkCore;
using RestfulAPI.Modal;
using RestfulAPI.Repos;
using RestfulAPI.Repos.Models;
using RestfulAPI.Service.Interfaces;
using Sieve.Models;
using Sieve.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RestfulAPI.Service.Implementations
{
    public class TracksService : ITracksService
    {
        private readonly LearndataContext context;
        private readonly IMapper mapper;
        private readonly ISieveProcessor sieveProcessor;

        public TracksService(LearndataContext context, IMapper mapper, ISieveProcessor sieveProcessor)
        {
            this.context = context;
            this.mapper = mapper;
            this.sieveProcessor = sieveProcessor;
        }

        public async Task<List<TracksModal>> SearchTracksAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return await Getall(); // nếu không nhập keyword, trả về tất cả

            var allTracks = await Getall();

            // filter theo title, artist hoặc genre
            var filtered = allTracks
                .Where(track =>
                    track.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                    || track.Artists.Any(a => a.ArtistName.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                )
                .ToList();

            return filtered;
        }

        // TracksService.cs
        public async Task<TracksModal> GetByIdAsync(string id)
        {
            var track = await context.Tracks
                .Where(t => t.TrackId == id)
                .Select(t => new TracksModal
                {
                    TrackId = t.TrackId,
                    Title = t.Title,
                    FileUrl = t.FileUrl,
                    TrackImageUrl = t.TrackImageUrl,
                    DurationSeconds = t.DurationSeconds,
                    AlbumTitle = t.Album != null ? t.Album.Title : null,

                    Artists = t.TrackArtists.Select(a => new ArtistModal
                    {
                        ArtistId = a.Artist.ArtistId,
                        ArtistName = a.Artist.ArtistName,
                        ArtistImageUrl = a.Artist.ArtistImageUrl
                    }).ToList(),

                    Genres = t.TrackGenres.Select(g => new GenreModal
                    {
                        GenreId = g.Genre.GenreId,
                        GenreName = g.Genre.Name
                    }).ToList(),

                    Categories = t.TrackCategories.Select(c => new CategoryModal
                    {
                        CategoryId = c.Category.CategoryId,
                        CategoryName = c.Category.Name
                    }).ToList()
                })
                .AsNoTracking() // Không track để tăng tốc độ
                .FirstOrDefaultAsync();

            return track;
        }

        public async Task<List<TracksModal>> Getall()
        {
            List<TracksModal> _response = new List<TracksModal>();

            var _data = await context.Tracks
                .Include(t => t.TrackArtists)     // load bảng trung gian TrackArtist
                    .ThenInclude(ta => ta.Artist) // load Artist bên trong TrackArtist
                .Include(t => t.TrackGenres)      // load bảng trung gian TrackGenre
                    .ThenInclude(tg => tg.Genre)  // load Genre bên trong TrackGenre
                .Include(t => t.Album)            // load Album
                .AsSplitQuery()
                .Where(t => t.TrackId != null)
                .ToListAsync();

            if (_data != null && _data.Count > 0)
            {
                //logger.LogInformation("GetAllTracks begins");
                return mapper.Map<List<Track>, List<TracksModal>>(_data);
            }

            return new List<TracksModal>();
        }

        public async Task<List<TracksModal>> GetByGenreAsync(string genreName, int limit = 10)
        {
            var tracks = await context.Tracks
                .AsNoTracking()
                .Where(t => t.TrackGenres.Any(g => g.Genre.Name == genreName))
                .OrderBy(t => t.Title)
                .Select(t => new TracksModal
                {
                    TrackId = t.TrackId,
                    Title = t.Title,
                    TrackImageUrl = t.TrackImageUrl,
                    DurationSeconds = t.DurationSeconds,
                    Artists = t.TrackArtists.Select(a => new ArtistModal
                    {
                        ArtistName = a.Artist.ArtistName
                    }).ToList()
                })
                .Take(limit)
                .ToListAsync();

            return tracks;
        }

        public async Task<List<TracksModal>> GetByCategoryAsync(string categoryName, int limit = 10)
        {
            var tracks = await context.Tracks
                .AsNoTracking()
                .Where(t => t.TrackCategories.Any(c => c.Category.Name == categoryName))
                .OrderBy(t => t.Title)
                .Select(t => new TracksModal
                {
                    TrackId = t.TrackId,
                    Title = t.Title,
                    TrackImageUrl = t.TrackImageUrl,
                    DurationSeconds = t.DurationSeconds,
                    Artists = t.TrackArtists.Select(a => new ArtistModal
                    {
                        ArtistName = a.Artist.ArtistName
                    }).ToList()
                })
                .Take(limit)
                .ToListAsync();

            return tracks;
        }

        public async Task<List<string>> GetRandomGenresAsync(int count = 3)
        {
            return await context.Genres
                .AsNoTracking()
                .OrderBy(g => Guid.NewGuid()) // random
                .Take(count)
                .Select(g => g.Name)
                .ToListAsync();
        }

        public async Task<List<string>> GetRandomCategoriesAsync(int count = 3)
        {
            return await context.Category
                .AsNoTracking()
                .OrderBy(c => Guid.NewGuid()) // random
                .Take(count)
                .Select(c => c.Name)
                .ToListAsync();
        }
        public async Task<Dictionary<string, List<TracksModal>>> GetHomeTracksAsync()
        {
            var result = new Dictionary<string, List<TracksModal>>();

            var randomGenres = await GetRandomGenresAsync(2);
            var randomCategories = await GetRandomCategoriesAsync(1);

            foreach (var genre in randomGenres)
            {
                result.Add(genre, await GetByGenreAsync(genre, 5));
            }

            foreach (var category in randomCategories)
            {
                result.Add(category, await GetByCategoryAsync(category, 5));
            }

            return result;
        }
    }
}
