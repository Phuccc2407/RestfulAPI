using Microsoft.AspNetCore.Identity;
using RestfulAPI.Modal;
using RestfulAPI.Repos.Models;
using RestfulAPI.Service.Interfaces;

namespace RestfulAPI.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _env;

        public UserService(UserManager<User> userManager, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _env = env;
        }

        public async Task<UserModal?> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return null;

            return new UserModal
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };
        }

        public async Task<bool> UploadAvatarAsync(IFormFile avatar, string userName)
        {
            if (avatar == null || avatar.Length == 0) return false;

            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return false;

            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "images");
            Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{user.Id}_{Path.GetFileName(avatar.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await avatar.CopyToAsync(stream);
            }

            user.AvatarUrl = $"/uploads/images/{fileName}";
            await _userManager.UpdateAsync(user);

            return true;
        }
    }
}
