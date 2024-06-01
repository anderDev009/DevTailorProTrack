using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TailorProTrack.Api.Utils;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;

namespace TailorProTrack.Api.Controllers
{
    [ApiController]
    [Route("api/InventoryColor/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class InventoryColorController : Controller
    {
        private readonly IInventoryColorService _inventoryColorService;

        public InventoryColorController(IInventoryColorService invColorService)
        {
            _inventoryColorService = invColorService;            
        }

        [HttpGet("GetInventoryColors")]
        public IActionResult Index([FromQuery] PaginationParams @params)
        {
            ServiceResultWithHeader result = this._inventoryColorService.GetAll(@params);

            ServiceResult response = new ServiceResult { Data = result.Data, Message = result.Message, Success = result.Success };

            if (!result.Success)
            {
                return BadRequest(response);
            }
            Response.AddHeaderPaginationJson(result.Header);
            return Ok(response);
        }

        [HttpGet("GetInventoryColorById")]
        public IActionResult GetById(int id)
        {
            var result = _inventoryColorService.GetById(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
