using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TailorProTrack.Application.Contracts.Color;

namespace TailorProTrack.Api.Controllers.Filters
{
    [ApiController]
    [Route("api/filter/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class ColorFilterController : Controller
    {
        private readonly IColorFilterService _colorFilterService;

        public ColorFilterController(IColorFilterService colorFilterService)
        {
            _colorFilterService = colorFilterService;   
        }

        [HttpGet("FilterByName")]
        public IActionResult GetByName([FromQuery] string colorName) 
        {
            var result = _colorFilterService.FilterByName(colorName);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("FilterByColorCode")]
        public IActionResult GetByColorCode([FromQuery] string colorCode)
        {
            var result = _colorFilterService.FilterByColorCode(colorCode);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
