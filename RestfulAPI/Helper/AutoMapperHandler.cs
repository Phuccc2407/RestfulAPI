using AutoMapper;
using RestfulAPI.Modal;
using RestfulAPI.Repos.Models;

namespace RestfulAPI.Helper
{
    public class AutoMapperHandler : Profile
    {
        public AutoMapperHandler()
        {
            CreateMap<Track, TracksModal>()
                .ForMember(dest => dest.Artists, opt =>
                    opt.MapFrom(src => src.TrackArtists != null
                        ? src.TrackArtists.Select(ta => new ArtistModal
                        {
                            ArtistId = ta.Artist.ArtistId,
                            ArtistName = ta.Artist.ArtistName,
                            ArtistImageUrl = ta.Artist.ArtistImageUrl
                        })
                        : new List<ArtistModal>()))
                .ForMember(dest => dest.Genres, opt =>
                    opt.MapFrom(src => src.TrackGenres != null
                        ? src.TrackGenres.Select(tg => new GenreModal
                        {
                            GenreId = tg.Genre.GenreId,
                            GenreName = tg.Genre.Name
                        })
                        : new List<GenreModal>()))
                .ForMember(dest => dest.Categories, opt =>
                    opt.MapFrom(src => src.TrackCategories != null
                        ? src.TrackCategories.Select(tc => new CategoryModal
                        {
                            CategoryId = tc.Category.CategoryId,
                            CategoryName = tc.Category.Name
                        })
                        : new List<CategoryModal>()))
                .ForMember(dest => dest.AlbumTitle, opt =>
                    opt.MapFrom(src => src.Album != null ? src.Album.Title : null));
        }
    }
}
