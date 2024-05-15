using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.BankAccount;

namespace TailorProTrack.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class BankAccountController : Controller
    {
        private readonly IBankAccountService _bankAccountService;

        public BankAccountController(IBankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService;
        }

        [HttpGet("GetBankAccounts")]
        public IActionResult Index([FromQuery] PaginationParams @params)
        {
            var result = _bankAccountService.GetAll(@params);
            if (!result.Success)
            {
                return StatusCode(500, result);
            }
            return Ok(result);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var result = _bankAccountService.GetById(id);
            if (!result.Success)
            {
                return StatusCode(500, result);
            }
            return Ok(result);
        
    }

        [HttpPost("SaveBankAccount")]
        public IActionResult Save([FromBody] BankAccountDtoAdd dtoAdd)
        {
            var result = _bankAccountService.Add(dtoAdd);
            if (!result.Success)
            {
                return StatusCode(500, result);
            }
            return Ok(result);
        }

        [HttpPut("UpdateBankAccount")]
        public IActionResult Update([FromBody] BankAccountDtoUpdate dtoUpdate)
        {
            var result = _bankAccountService.Update(dtoUpdate);
            if (!result.Success)
            {
                return StatusCode(500, result);
            }
            return Ok(result);
        
        }
    }
}
