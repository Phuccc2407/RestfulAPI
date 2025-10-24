using Microsoft.AspNetCore.Identity;
using RestfulAPI.Modal;
using RestfulAPI.Repos.Models;
using RestfulAPI.Service.Interfaces;

namespace RestfulAPI.Service.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;

        public AuthService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> RegisterUser(LoginModal user)
        {
            var identityUser = new User
            {
                UserName = user.UserName,
                Email = user.Email
            };

            var result = await _userManager.CreateAsync(identityUser, user.Password);
            return result.Succeeded;
        }
    }
}
