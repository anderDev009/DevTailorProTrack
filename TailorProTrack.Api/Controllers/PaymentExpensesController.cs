using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TailorProTrack.Api.Utils;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Expenses.PaymentExpense;

namespace TailorProTrack.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class PaymentExpensesController : Controller
    {
        private readonly IPaymentExpensesService _paymentExpenseService;

        public PaymentExpensesController(IPaymentExpensesService service)
        {
            _paymentExpenseService = service;
        }

        [HttpGet("GetPaymentsExpenses")]
        [ProducesResponseType(StatusCodes.Status200OK,Type= typeof(List<PaymentExpenseDtoGet>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Index([FromQuery] PaginationParams @params)
        {
            var result = this._paymentExpenseService.GetAllWithInclude(@params,new List<string>{ "Expense", "PaymentType", "BankAccount" });
            ServiceResult response = new ServiceResult { Data = result.Data, Message = result.Message, Success = result.Success };
            if (!response.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
            Response.AddHeaderPaginationJson(result.Header);
            return Ok(response);
        }

        [HttpGet("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PaymentExpenseDtoGet>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetById(int id)
        {
            var result = this._paymentExpenseService.GetByIdWithInclude(id, new List<string>{ "Expense", "PaymentType", "BankAccount" });
            if (!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,result);
            }
            return Ok(result);
        }

        [HttpPost("SavePaymentExpense")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Save([FromBody] PaymentExpenseDtoAdd dtoAdd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _paymentExpenseService.Add(dtoAdd);
            if (!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }

            return Created();
        }

        [HttpPut("UpdatePaymentExpense")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update([FromBody] PaymentExpenseDtoUpdate dtoUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _paymentExpenseService.Update(dtoUpdate,dtoUpdate.Id);
            if (!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            var result = _paymentExpenseService.Remove(id);
            if (!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }

            return NoContent();
        }
    }
}
