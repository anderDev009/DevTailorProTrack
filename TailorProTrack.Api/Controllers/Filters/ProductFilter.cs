using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TailorProTrack.Application.Contracts.Client;

namespace TailorProTrack.Api.Controllers.Filters
{
    [ApiController]
    [Route("api/filter/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class ProductFilter : Controller
    {

        private readonly IProductFilterService _filterService;

        public ProductFilter(IProductFilterService filterService) 
        { 
           _filterService = filterService;
        }

        [HttpGet("FilterByHigherPrice")]
        public IActionResult GetByHigherPrice(decimal price)
        {
            var result = this._filterService.GetByHigherPrice(price);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("FilterByMinorPrice")]
        public IActionResult GetByMinorPrice(decimal price)
        {
            var result = this._filterService.GetByMinorPrice(price);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("FilterByType")]
        public IActionResult GetByType(int fkType)
        {
            var result = this._filterService.SearchByType(fkType);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("FilterByName")]
        public IActionResult GetByName(string name)
        {
            var result = this._filterService.SearchByProductName($"%{name}%");
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
