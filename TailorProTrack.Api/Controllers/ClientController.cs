using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using TailorProTrack.Api.Utils;
using TailorProTrack.Application.Contracts.Client;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Client;
namespace TailorProTrack.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class ClientController : Controller
    {
        private readonly IClientService service;

        public ClientController(IClientService service)
        {
            this.service = service;
        }

        [HttpGet("GetClients")]
        public IActionResult Index([FromQuery] PaginationParams @params)
        {
            var result = service.GetAll(@params);
            
            ServiceResult response = new ServiceResult { Data = result.Data, Message = result.Message, Success = result.Success};

            if (!result.Success)
            {
                return BadRequest(response);
            }
            Response.AddHeaderPaginationJson(result.Header);
            return Ok(response);
        }

        [HttpGet("GetAllClients")]
        public IActionResult GetAll()
        {
            var result = service.GetAll();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("GetClient")]
        public IActionResult Get(int id)
        {
            var result = service.GetById(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("SaveClient")]
        public IActionResult Post([FromBody] ClientDtoAdd dtoAdd)
        {
            var result = service.Add(dtoAdd);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("UpdateClient")]
        public IActionResult Put([FromBody] ClientDtoUpdate dtoUpdate)
        {
            var result = service.Update(dtoUpdate);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("RemoveClient")]
        public IActionResult Delete([FromBody] ClientDtoRemove dtoRemove) 
        {
            var result = service.Remove(dtoRemove);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
