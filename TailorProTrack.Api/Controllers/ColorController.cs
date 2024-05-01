using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TailorProTrack.Api.Utils;
using TailorProTrack.Application.Contracts.Color;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Color;

namespace TailorProTrack.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class ColorController : Controller
    {
        
        private readonly IColorService _service;

        public ColorController(IColorService service)
        {
            this._service = service;
        }


        // GET: Colors

        [HttpGet("GetColors")]
        public ActionResult Index([FromQuery] PaginationParams @params)
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

        // GET: Color
        [HttpGet("GetColor")]
        public ActionResult Get(int id)
        {
            var result = this._service.GetById(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // POST: Insert color
        [HttpPost("SaveColor")]
        public ActionResult Add([FromBody] ColorDtoAdd colorDtoAdd) 
        {
            var result = this._service.Add(colorDtoAdd);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // Actualizar colores
        [HttpPut("UpdateColor")]
        public ActionResult Update([FromBody] ColorDtoUpdate colorDtoUpdate)
        {
            var result = this._service.Update(colorDtoUpdate);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        //obtener colores asociados 
        [HttpGet("GetColorsAsociatedById")]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(List<ColorDtoGet>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetColorsAsociatedById(int id)
        {
            try
            {
                var result = _service.GetColorsAsociatedByProductId(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }
   
    

     
    }
}
