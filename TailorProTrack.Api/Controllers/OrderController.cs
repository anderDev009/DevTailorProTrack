using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TailorProTrack.Api.Utils;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Order;
using TailorProTrack.Application.Dtos.PreOrder;

namespace TailorProTrack.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet("GetOrders")]
        public IActionResult Get([FromQuery] PaginationParams @params)
        {
            ServiceResultWithHeader result = this.orderService.GetAll(@params);

            ServiceResult response = new ServiceResult { Data = result.Data, Message = result.Message, Success = result.Success };


            if (!result.Success)
            {
                return BadRequest(response);
            }
            Response.AddHeaderPaginationJson(result.Header);
            return Ok(response);
        }
        [HttpGet("GetOrderJobs")]
        public IActionResult GetOrderJobs([FromQuery] PaginationParams @params)
        {
            ServiceResultWithHeader result = this.orderService.GetOrderJobs(@params);
            ServiceResult response = new ServiceResult { Data = result.Data, Message = result.Message, Success = result.Success };

            if (!result.Success)
            {
                return BadRequest(response);
            }
            Response.AddHeaderPaginationJson(result.Header);
            return Ok(response);
        }
        [HttpGet("GetOrder")]
        public IActionResult GetOrder(int id)
        {
            ServiceResult result = this.orderService.GetOrder(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("GetInvColorAvailableToAddOrder")]
        public IActionResult GetAvailable(List<PreOrderDtoFkSizeFkProduct> dto)
        {
            ServiceResult result = this.orderService.GetInvColorAvailableToAddOrder(dto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("GetOrderDetail")]
        public IActionResult GetOrderDetail(int id)
        {
            ServiceResult result = this.orderService.GetById(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("AddOrder")]
        public IActionResult Add([FromBody] OrderDtoAdd dtoAdd)
        {
            ServiceResult result = this.orderService.Add(dtoAdd);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("UpdateStatusOrder")]
        public IActionResult PutStatusOrder([FromBody] OrderDtoUpdateStatus dtoUpdateStatus)
        {
            ServiceResult result = this.orderService.UpdateStatusOrder(dtoUpdateStatus);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
