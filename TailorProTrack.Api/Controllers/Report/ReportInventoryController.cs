using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Contracts.Report;

namespace TailorProTrack.Api.Controllers.Report
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class ReportInventoryController : Controller
    {
        private readonly IReportOrderInventoryService _reportOrderInventoryService;
        private readonly IPreOrderProductService _preorProductService;

        public ReportInventoryController(IReportOrderInventoryService reportOrderInventoryService, IPreOrderProductService preOrderProductService)
        {
            _reportOrderInventoryService = reportOrderInventoryService;
            _preorProductService = preOrderProductService;
        }

        [HttpGet("GetMissedInventory")]
        public IActionResult GetMissedInventory()
        {
            var result = _reportOrderInventoryService.GetMissedInventory();

            if (!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,result);
            }
            return Ok(result);
        }

        [HttpGet("GetReportDiffItemsById")]
        public IActionResult GetById(int preorderId)
        {
            var result = _reportOrderInventoryService.GetDiffItemsByPreOrderId(preorderId);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("GetReportDiffItems")]
        public IActionResult GetAll()
        {
            var result = _preorProductService.GetDiffItems();

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
