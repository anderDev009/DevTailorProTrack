using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TailorProTrack.Application.Contracts.Report;

namespace TailorProTrack.Api.Controllers.Report
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class ReportInventoryController : Controller
    {
        private readonly IReportOrderInventoryService _reportOrderInventoryService;

        public ReportInventoryController(IReportOrderInventoryService reportOrderInventoryService)
        {
            _reportOrderInventoryService = reportOrderInventoryService;
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
    }
}
