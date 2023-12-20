using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Dtos.Size;

namespace TailorProTrack.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("*")]
    public class SizeController : Controller
    {
        private readonly ISizeService _service;

        public SizeController(ISizeService service)
        {
            this._service = service;
        }

        [HttpGet("GetSizes")]
        public IActionResult Index()
        {
            var result = this._service.GetAll();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
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
    }
}
