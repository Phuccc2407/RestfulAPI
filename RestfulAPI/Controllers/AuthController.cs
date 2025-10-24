using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestfulAPI.Modal;
using RestfulAPI.Service.Interfaces;

namespace RestfulAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<bool> RegisterUser(LoginModal user)
        {
            return await _authService.RegisterUser(user);
        }

        [HttpGet]
        public async Task Login(LoginModal user)
        {

        }
    }
}
