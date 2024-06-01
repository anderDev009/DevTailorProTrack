using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TailorProTrack.Application.Contracts.Client;

namespace TailorProTrack.Api.Controllers.Filters
{
    [ApiController]
    [Route("api/filter/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class ClientFilterController : Controller
    {
        private readonly IClientFilterService _filterService;
        public ClientFilterController(IClientFilterService filterService)
        {
            _filterService = filterService;
        }

        [HttpGet("FilterByFullName")]
        public IActionResult GetByName([FromQuery] string fullName)
        {
            var result = _filterService.FilterByFullName(fullName);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("FilterByDni")]
        public IActionResult GetByDni([FromQuery] string dni)
        {
            var result = _filterService.FilterByDni(dni);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("FilterByRnc")]
        public IActionResult GetByRnc([FromQuery] string rnc)
        {
            var result = _filterService.FilterByRnc(rnc);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
