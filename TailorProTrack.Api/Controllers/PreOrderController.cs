﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.PreOrder;
using TailorProTrack.Application.Dtos.PreOrderProducts;

namespace TailorProTrack.Api.Controllers
{
    [ApiController]
    [Route("api/preorder/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class PreOrderController : Controller
    {
        private IPreOrderService _preOrderService;
        private IPreOrderProductService _preOrderProductService;

        public PreOrderController(IPreOrderService preOrderService, IPreOrderProductService preOrderProductService)
        {
            _preOrderService = preOrderService;
            _preOrderProductService = preOrderProductService;
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
        [HttpPut("UpdatePreOrderProducts")]
        public IActionResult UpdatePreOrderProducts([FromBody] PreOrderProductsDtoUpdate dtoUpdate)
        {
            var result = _preOrderProductService.Update(dtoUpdate);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("RemovePreOrder")]
        public IActionResult DeleteById(int id)
        {
            var result = _preOrderService.Remove(new PreOrderDtoRemove(){Date = DateTime.UtcNow,Id = id,User = 1});
            if (!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,result.Message);
            }
            return NoContent();
        }

        [HttpGet("GetAmountPendingByPreOrderId")]
        public IActionResult GetAmountPendingByPreOrderId(int id)
		{
			var result = _preOrderProductService.GetAmountByIdPreOrder(id);
			return Ok(result);
		}
	}
}
