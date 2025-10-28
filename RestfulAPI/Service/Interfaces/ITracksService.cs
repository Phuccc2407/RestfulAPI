using RestfulAPI.Modal;
using Sieve.Models;

namespace RestfulAPI.Service.Interfaces
{
    public interface ITracksService
    {
        Task<List<TracksModal>> Getall();
        Task<TracksModal> GetByIdAsync(string id);
        Task<List<TracksModal>> SearchTracksAsync(string keyword);
        Task<List<string>> GetRandomGenresAsync(int count = 3);
        Task<List<string>> GetRandomCategoriesAsync(int count = 3);
        Task<Dictionary<string, List<TracksModal>>> GetHomeTracksAsync();
    }
}
