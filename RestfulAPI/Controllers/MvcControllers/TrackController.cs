using Microsoft.AspNetCore.Mvc;
using RestfulAPI.Modal;
using RestfulAPI.Repos;
using RestfulAPI.Service.Interfaces;
using System.Net.Http.Json;

namespace RestfulAPI.Controllers.MvcControllers
{
    public class TrackController : Controller
    {
        private readonly ITracksService _tracksService;

        public TrackController(ITracksService tracksService)
        {
            _tracksService = tracksService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Detail(string id)
        {
            var track = await _tracksService.GetByIdAsync(id);
            if (track == null)
                return NotFound();

            return View(track);
        }
    }
}
