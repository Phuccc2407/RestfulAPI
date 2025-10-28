using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestfulAPI.Service.Interfaces;

namespace RestfulAPI.Controllers.MvcControllers
{
    [Authorize(Roles = "ADMIN")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet("users")]
        public IActionResult Users()
        {
            return View();
        }

        [HttpGet("tracks")]
        public IActionResult Tracks()
        {
            return View();
        }
    }
}
