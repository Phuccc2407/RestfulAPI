using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestfulAPI.Modal;
using RestfulAPI.Repos.Models;
using RestfulAPI.Service.Interfaces;

namespace RestfulAPI.Controllers.MvcControllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet("/user/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var model = await _userService.GetUserByIdAsync(id);
            if (model == null) return NotFound();

            return View("Profile", model);
        }

        [Authorize]
        [HttpPost("/user/upload-avatar")]
        public async Task<IActionResult> UploadAvatar(IFormFile avatar)
        {
            var userName = User.Identity?.Name;
            var success = await _userService.UploadAvatarAsync(avatar, userName ?? "");

            if (!success) return BadRequest("Không thể upload ảnh.");

            var user = await _userService.GetUserByIdAsync(userName ?? "");
            return Redirect($"/user/{user?.UserId}");
        }
    }

}
