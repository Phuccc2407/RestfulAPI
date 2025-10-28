using Microsoft.AspNetCore.Mvc;
using RestfulAPI.Service.Interfaces;

namespace RestfulAPI.Controllers.MvcControllers
{
    public class HomeController : Controller
    {
        private readonly ITracksService tracksService;

        public HomeController(ITracksService tracksService)
        {
            this.tracksService = tracksService;
        }

        public async Task<IActionResult> Index()
        {
            var homeTracks = await tracksService.GetHomeTracksAsync();
            return View(homeTracks);
        }
    }
}
