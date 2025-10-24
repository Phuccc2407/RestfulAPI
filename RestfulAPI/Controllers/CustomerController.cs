using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using RestfulAPI.Modal;
using RestfulAPI.Service.Interfaces;

namespace RestfulAPI.Controllers
{
    [EnableRateLimiting("Fixedwindow")]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService service;

        public CustomerController(ICustomerService service) {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await this.service.Getall();
            if(data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpGet("GetbyGUID")]
        public async Task<IActionResult> GetbyGUID(Guid guid)
        {
            var data = await this.service.GetbyGUID(guid);
            if(data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CustomerModal _data)
        {
            var data = await this.service.Create(_data);
            return Ok(data);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(CustomerModal _data, Guid guid)
        {
            var data = await this.service.Update(_data, guid);
            return Ok(data);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid guid)
        {
            var data = await this.service.Remove(guid);
            return Ok(data);
        }
    }
}
