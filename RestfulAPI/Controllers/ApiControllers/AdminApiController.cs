using Microsoft.AspNetCore.Mvc;
using RestfulAPI.Service.Interfaces;

namespace RestfulAPI.Controllers.ApiControllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminApiController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminApiController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            var users = await _adminService.GetTotalUsersAsync();
            var tracks = await _adminService.GetTotalTracksAsync();

            return Ok(new
            {
                totalUsers = users,
                totalTracks = tracks
            });
        }
    }

}
