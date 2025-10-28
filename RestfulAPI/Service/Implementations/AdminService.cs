using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestfulAPI.Repos;
using RestfulAPI.Repos.Models;
using RestfulAPI.Service.Interfaces;

namespace RestfulAPI.Service.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly LearndataContext _context;

        public AdminService(LearndataContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalTracksAsync()
        {
            return await _context.Tracks.CountAsync();
        }

        public async Task<int> GetTotalUsersAsync()
        {
            return await _context.Users.CountAsync();
        }
    }

}
