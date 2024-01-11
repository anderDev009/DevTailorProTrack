using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TailorProTrack.Api.Utils;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.TypeProd;

namespace TailorProTrack.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class TypeProdController : Controller
    {
        private readonly ITypeProdService _service;

        public TypeProdController(ITypeProdService typeProdService)
        {
            this._service = typeProdService;
        }

        [HttpGet("GetTypes")]
        public ActionResult Index([FromQuery] PaginationParams @params)
        {
            var result = this._service.GetAll(@params);
            ServiceResult response = result;

            if (!result.Success)
            {
                return BadRequest(response);
            }
            Response.AddHeaderPaginationJson(result.Header);
            return Ok(response);
        }

        [HttpGet("GetType")]
        public ActionResult Get(int id)
        {
            var result = this._service.GetById(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("AddType")]
        public ActionResult Create([FromBody] TypeProdDtoAdd typeProdDtoAdd)
        {
            var result = this._service.Add(typeProdDtoAdd);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("UpdateType")]
        public ActionResult Put([FromBody] TypeProdDtoUpdate typeProdDtoUpdate)
        {
            var result = this._service.Update(typeProdDtoUpdate);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("RemoveType")]
        public ActionResult Delete([FromBody] TypeProdDtoRemove typeProdDtoRemove)
        {
            var result = this._service.Remove(typeProdDtoRemove);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }  
}
