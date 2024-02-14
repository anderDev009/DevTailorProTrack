using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.PreOrder;

namespace TailorProTrack.Api.Controllers
{
    [ApiController]
    [Route("api/preorder/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class PreOrderController : Controller
    {
        private IPreOrderService _preOrderService;

        public PreOrderController(IPreOrderService preOrderService)
        {
            _preOrderService = preOrderService;
        }
        [HttpGet("GetPreOrders")]
        public IActionResult Get([FromQuery] PaginationParams @params)
        {
            var result = _preOrderService.GetAll(@params);
            ServiceResult response = new ServiceResult { Data = result.Data, Message = result.Message, Success = result.Success };

            if (!result.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("GetPreOrder")]
        public IActionResult GetById(int id)
        {
            var result = _preOrderService.GetById(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("AddPreOrder")]
        public IActionResult Save([FromBody] PreOrderDtoAdd dtoAdd)
        {
            var result = _preOrderService.Add(dtoAdd);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
