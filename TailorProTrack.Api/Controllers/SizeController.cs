using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TailorProTrack.Api.Utils;
using TailorProTrack.Application.Contracts.Size;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Sale;
using TailorProTrack.Application.Dtos.Size;

namespace TailorProTrack.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class SizeController : Controller
    {
        private readonly ISizeService _service;

        public SizeController(ISizeService service)
        {
            this._service = service;
        }

        [HttpGet("GetSizes")]
        public IActionResult Index([FromQuery] PaginationParams @params)
        {
            var result = this._service.GetAll(@params);
            ServiceResult response = new ServiceResult { Data = result.Data, Message = result.Message, Success = result.Success };


            if (!result.Success)
            {
                return BadRequest(response);
            }
            Response.AddHeaderPaginationJson(result.Header);
            return Ok(response);
        }
        [HttpGet("GetSize")]
        public IActionResult Get(int id) 
        {
            var result = this._service.GetById(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        //sizes disponibles por id de producto
        [HttpGet("GetSizesAvailablesByIdProd")]
        public IActionResult GetAvailables(int id)
        {
            var result = this._service.GetSizesAvailablesProductById(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("GetSizesByCateogoryId")]
        public IActionResult GetByCategory(int CategoryId)
        {
            var result = this._service.GetSizesByCategoryId(CategoryId);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("SaveSize")]
        public IActionResult Post([FromBody] SizeDtoAdd sizeDtoAdd)
        {
            var result = this._service.Add(sizeDtoAdd);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("UpdateSize")]
        public IActionResult Put([FromBody] SizeDtoUpdate sizeDtoUpdate)
        {
            var result = this._service.Update(sizeDtoUpdate);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("GetSizesAsociatedByProId")]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(List<SizeDtoGetMapped>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetSizesAsociated(int prodId)
        {

            var result = this._service.GetSizesAsociatedByProdId(prodId);
            if (!result.Success || result.Data == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        
    }
}
