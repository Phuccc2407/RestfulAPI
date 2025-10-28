using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RestfulAPI.Modal;
using RestfulAPI.Repos;
using RestfulAPI.Repos.Models;
using RestfulAPI.Service.Interfaces;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestfulAPI.Service.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly LearndataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        private readonly SignInManager<User> _signInManager;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration config, LearndataContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _context = context;
        }

        public async Task<bool> RegisterUser(RegisterModal user)
        {
            var identityUser = new User
            {
                UserName = user.UserName,
                Email = user.Email,
                AvatarUrl = "/uploads/images/useravatar.jpg"
            };

            var result = await _userManager.CreateAsync(identityUser, user.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(identityUser, "User");
            }

            return result.Succeeded;
        }
        public async Task<bool> LoginWithCookie(LoginModal login)
        {
            var identityUser = await _userManager.FindByNameAsync(login.UserName);
            if (identityUser == null)
                return false;

            // SignInManager sẽ check password và tạo cookie
            var result = await _signInManager.PasswordSignInAsync(
                login.UserName,
                login.Password,
                isPersistent: true,  // true: cookie tồn tại lâu dài
                lockoutOnFailure: false
            );

            return result.Succeeded;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
