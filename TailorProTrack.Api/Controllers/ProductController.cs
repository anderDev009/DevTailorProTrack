using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TailorProTrack.Api.Utils;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Product;

namespace TailorProTrack.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class ProductController : Controller
    {
        private readonly IProductService _Productservice;

        public ProductController(IProductService productservice)
        {
            _Productservice = productservice;
        }

        [HttpGet("GetProducts")]
        public IActionResult Get([FromQuery] PaginationParams @params)
        {
            var result = this._Productservice.GetAll(@params);
            ServiceResult response = result;

            if (!result.Success)
            {
                return BadRequest(response);
            }
            Response.AddHeaderPaginationJson(result.Header);
            return Ok(response);
        }

        [HttpGet("GetProduct")]
        public IActionResult Get(int id)
        {
            var result = this._Productservice.GetById(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("SaveProduct")]
        public IActionResult Post([FromBody] ProductDtoAdd productDtoAdd)        
        {
            var result = this._Productservice.Add(productDtoAdd);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut("UpdateProduct")]
        public IActionResult Put([FromBody] ProductDtoUpdate productDtoUpdate)
        {
            var result = this._Productservice.Update(productDtoUpdate);
            if (!result.Success)
            {
                return BadRequest(result);  
            }
            return Ok(result);
        }
        [HttpDelete("RemoveProduct")]
        public IActionResult Delete([FromBody] ProductDtoRemove productDtoRemove)
        {
            var result = this._Productservice.Remove(productDtoRemove);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
