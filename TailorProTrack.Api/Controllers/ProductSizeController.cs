using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TailorProTrack.Application.Contracts.ProductColor;
using TailorProTrack.Application.Contracts.ProductSize;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.ProductColor;
using TailorProTrack.Application.Dtos.ProductSize;
using TailorProTrack.Application.Service.ProductSizeService;

namespace TailorProTrack.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class ProductSizeController : Controller
    {
        private readonly IProductSizeService _productSizeService;
        public ProductSizeController(IProductSizeService productSizeService)
        {
            _productSizeService = productSizeService;
        }


        [HttpGet("GetProductsSize")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductColorDtoGet>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll([FromQuery] PaginationParams @params)
        {
            try
            {
                var productColors = _productSizeService.GetAll(@params);
                return Ok(productColors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("AddProductSize")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Add([FromBody] ProductSizeDtoAdd dtoAdd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                _productSizeService.Add(dtoAdd);
                return Created();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("UpdateProductSize")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateProductColor([FromBody] ProductSizeDtoUpdate dtoUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                _productSizeService.Update(dtoUpdate, dtoUpdate.Id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("RemoveProductSize")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Remove(int id)
        {
            try
            {
                _productSizeService.Remove(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
