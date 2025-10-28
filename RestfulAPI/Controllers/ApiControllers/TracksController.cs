using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using RestfulAPI.Service.Implementations;
using RestfulAPI.Service.Interfaces;
using Sieve.Models;

namespace RestfulAPI.Controllers.ApiControllers
{
    [EnableRateLimiting("Fixedwindow")]
    [Route("api/tracks")]
    [ApiController]
    public class TracksController : ControllerBase
    {
        private readonly ITracksService service;

        public TracksController(ITracksService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await service.Getall();
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var track = await service.GetByIdAsync(id);
            if (track == null)
            {
                return NotFound();
            }
            return Ok(track);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            var result = await service.SearchTracksAsync(keyword);
            return Ok(result);
        }
    }
}
