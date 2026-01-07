using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TailorProTrack.Api.Utils;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Suppliers;

namespace TailorProTrack.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class SupplierController : Controller
    {
        private readonly ISupplierService _supplierService;
        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }


        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll([FromQuery] PaginationParams @params)
        {
            ServiceResultWithHeader result = this._supplierService.GetAll(@params);

            ServiceResult response = new ServiceResult { Data = result.Data, Message = result.Message, Success = result.Success };

            if (!result.Success)
            {
                return BadRequest(response);
            }
            Response.AddHeaderPaginationJson(result.Header);
            return Ok(response);
        }


        [HttpGet("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetById(int id)
        {
            var result = this._supplierService.GetById(id);

            ServiceResult response = new ServiceResult { Data = result.Data, Message = result.Message, Success = result.Success };

            if (!result.Success)
            {
                return StatusCode(StatusCodes.Status400BadRequest, result.Message);
            }
            return Ok(response);
        }


        [HttpPost("Add")]
        public IActionResult Add([FromBody] SuppliersDtoAdd dtoAdd)
        {
            var result = _supplierService.Add(dtoAdd);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("Update")]
        public IActionResult Update([FromBody] SuppliersDtoUpdate dtoUpdate)
        {
            var result = _supplierService.Update(dtoUpdate,dtoUpdate.ID);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }

        [HttpDelete("Remove")]
        public IActionResult Remove([FromBody] SuppliersDtoGet supplier)
        {
            var result = _supplierService.Remove(supplier.ID);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
