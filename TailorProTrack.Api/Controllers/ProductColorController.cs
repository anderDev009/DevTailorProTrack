using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using TailorProTrack.Application.Contracts.ProductColor;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.ProductColor;

namespace TailorProTrack.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class ProductColorController : Controller
    {
        private readonly IProductColorService _productColorService;
        public ProductColorController(IProductColorService productColorService)
        {
           _productColorService = productColorService;
        }


        [HttpGet("GetProductsColor")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductColorDtoGet>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll([FromQuery]PaginationParams @params)
        {
            try
            {
                var productColors = _productColorService.GetAll(@params);
                return Ok(productColors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("AddProductColor")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public  IActionResult Add([FromBody] ProductColorDtoAdd dtoAdd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                _productColorService.Add(dtoAdd);
                return Created();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("UpdateProductColor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateProductColor([FromBody] ProductColorDtoUpdate dtoUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                _productColorService.Update(dtoUpdate, dtoUpdate.Id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("RemoveProductColor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Remove(int id)
        {
            try
            {
                _productColorService.Remove(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        
    }
}
