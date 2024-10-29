using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Contracts.Client;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Phone;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]

    public class PhoneController : Controller
    {
        private readonly IPhoneService _phoneService;
        private readonly IClientRepository _clientService;
        public PhoneController(IPhoneService phoneService, IClientRepository clientService)
        {
            _phoneService = phoneService;
            _clientService = clientService;
        }

        [HttpPost("client")]
        public IActionResult AddPhoneClient([FromBody] PhoneDtoAdd phoneDtoAdd)
        {
           
            if (_clientService.Exists(x=> x.ID == phoneDtoAdd.fk_client))
            {
                return BadRequest(new ServiceResult { Success = false, Data = null, Message = "Cliente no existe" });
            }
            var result = _phoneService.Add(phoneDtoAdd);
            
            if (!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
            return StatusCode(StatusCodes.Status201Created, result);
        }

       
    }
}
