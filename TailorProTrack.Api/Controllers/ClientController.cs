using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TailorProTrack.Application.Contracts;
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
        public IActionResult Index()
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
