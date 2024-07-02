using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Dtos.Payment;
using TailorProTrack.Application.Core;
using TailorProTrack.Api.Utils;

namespace TailorProTrack.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]

    public class PaymentController : Controller
    {
        private readonly IPaymentService _service;
        public PaymentController(IPaymentService service) 
        {
            _service = service;
        }

        [HttpGet("GetPayments")]
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

        [HttpGet("GetPaymetById")]
        public IActionResult GetPaymentById(int id)
        {
            var result = this._service.GetById(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("GetPaymentByOrderId")]
        public IActionResult GetPaymentByOrderId(int id)
        {
            var result = this._service.GetPaymentsByOrderId(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("AddPayment")]
        public IActionResult Add([FromBody] PaymentDtoAdd dtoAdd)
        {
            var result = this._service.Add(dtoAdd);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("UpdatePayment")]
        public IActionResult UpdatePayment([FromBody] PaymentDtoUpdate dtoUpdate)
        {
            var result = this._service.Update(dtoUpdate);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("RemovePayment")]
        public IActionResult RemovePayment([FromBody] PaymentDtoRemove dtoRemove)
        {
            var result = this._service.Remove(dtoRemove);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("GetAmountPendingByIdPreOrder")]
        public IActionResult GetAmountPendingByIdPreOrder(int id)
		{
			var result = this._service.GetAmountByIdOrder(id);
			return Ok(result);
		}

	}
}
