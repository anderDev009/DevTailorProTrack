using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop.Infrastructure;
using TailorProTrack.Api.Utils;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Bank;

namespace TailorProTrack.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class BankController : Controller
    {
        private readonly IBankService _bankService;

        public BankController(IBankService bankService)
        {
            _bankService = bankService;
        }

        [HttpGet("GetBanks")]
        public IActionResult Get([FromQuery] PaginationParams @params)
        {
            var result = this._bankService.GetAll(@params);
            ServiceResult response = new ServiceResult { Data = result.Data, Message = result.Message, Success = result.Success };
            if (!response.Success)
            {
                return BadRequest(response);
            }
            Response.AddHeaderPaginationJson(result.Header);
            return Ok(response);
        }

        [HttpGet("GetBankById")]
        public IActionResult GetById(int id)
        {
            var result = this._bankService.GetById(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("UpdateBank")]
        public IActionResult UpdateBank([FromBody] BankDtoUpdate dtoUpdate)
        {
            var result = this._bankService.Update(dtoUpdate);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("RemoveBank")]
        public IActionResult GetById([FromBody] BankDtoRemove dtoRemove)
        {
            var result = this._bankService.Remove(dtoRemove);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
