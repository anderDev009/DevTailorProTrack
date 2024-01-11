using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TailorProTrack.Api.Utils;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
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
        public ActionResult GetAll([FromQuery] PaginationParams @params)
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
