using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestfulAPI.Modal;
using RestfulAPI.Repos.Models;
using RestfulAPI.Service.Interfaces;

namespace RestfulAPI.Controllers.ApiControllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModal model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool result = await _authService.RegisterUser(model);
            if (!result)
                return BadRequest(new { message = "Đăng ký thất bại, username/email có thể đã tồn tại" });

            return Ok(new { message = "Đăng ký thành công" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModal login)
        {
            var success = await _authService.LoginWithCookie(login);
            if (!success) return Unauthorized("Sai thông tin đăng nhập");

            return Ok("Login thành công");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout();
            return Ok("Đã logout");
        }

        [Authorize(AuthenticationSchemes = "Identity.Application")]
        [HttpGet("me")]
        public async Task<IActionResult> Me([FromServices] UserManager<User> userManager)
        {
            var userName = User.Identity?.Name;
            if (userName == null) return Unauthorized();

            var user = await userManager.FindByNameAsync(userName);
            if (user == null) return Unauthorized();

            var roles = await userManager.GetRolesAsync(user);

            return Ok(new
            {
                userId = user.Id,
                userName = user.UserName,
                email = user.Email,
                avatarUrl = user.AvatarUrl,
                roles = roles.Select(r => r.ToLower())
            });
        }
    }
}
