using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Order;

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
        public IActionResult Get()
        {
            ServiceResult result = this.orderService.GetAll();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("GetOrderJobs")]
        public IActionResult GetOrderJobs()
        {
            ServiceResult result = this.orderService.GetOrderJobs();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
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
