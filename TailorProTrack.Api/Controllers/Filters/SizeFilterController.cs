using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TailorProTrack.Application.Contracts.Size;

namespace TailorProTrack.Api.Controllers.Filters
{
    [ApiController]
    [Route("api/filter/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class SizeFilterController : Controller
    {
        private readonly ISizeFilterService _sizeFilterService;

        public SizeFilterController(ISizeFilterService sizeFilterService)
        {
            _sizeFilterService = sizeFilterService;
        }

        [HttpGet("FilterByName")]
        public IActionResult GetByName([FromQuery] string name)
        {
            var result = _sizeFilterService.FilterByName(name);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("FilterByCategoryId")]
        public IActionResult GetByCategoryId([FromQuery] int categoryId)
        {
            var result = _sizeFilterService.FilterByIdCategory(categoryId);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
