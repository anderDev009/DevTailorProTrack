using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Dtos.PaymentType;

namespace TailorProTrack.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class PaymentTypeController : Controller
    {
        private readonly IPaymentTypeService _service;

        public PaymentTypeController(IPaymentTypeService service)
        {
            _service = service;
        }


        [HttpGet("GetPaymentTypes")]
        public ActionResult GetAll()
        {
            var result = this._service.GetAll();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // GET: PaymentTypeController/Details/5
        [HttpGet("GetPaymentType")]
        public ActionResult Details(int id)
        {
            var result = this._service.GetById(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // GET: PaymentTypeController/Create
        [HttpPost("AddPaymentType")]
        public ActionResult Add([FromBody] PaymentTypeDtoAdd dtoAdd)
        {
            var result = this._service.Add(dtoAdd);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("UpdatePaymentType")]
        public ActionResult Update([FromBody] PaymentTypeDtoUpdate dtoUpdate)
        {
            var result = this._service.Update(dtoUpdate);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("RemovePaymentType")]
        public ActionResult Edit([FromBody] PaymentTypeDtoRemove dtoRemove)
        {
            var result = this._service.Remove(dtoRemove);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }
}
