using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Service;

namespace TailorProTrack.Api.Controllers.Report
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class ReportsBalanceController : Controller
    {
        private readonly IPreOrderService _preOrderService;
        private readonly IExpensesService _expensesService;
        public ReportsBalanceController(IPreOrderService preOrderService, IExpensesService expensesService)
        {
            _preOrderService = preOrderService;
            _expensesService = expensesService;

        }
        [HttpGet("GetAccountsReceivable")]
        public IActionResult Index()
        {
            var result = _preOrderService.GetAccountsReceivable();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("GetPreOrdersByRecentDate")]
        public IActionResult GetPreOrdersByRecentDate()
        {
            var result = _preOrderService.GetPreOrdersByRecentDate();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        //[HttpGet("GetAccountsPayable")]
        //public IActionResult GetAccountsPayable()
        //{
        //    var result = _expensesService.GetAccountsPayable();
        //    if (!result.Success)
        //    {
        //        return BadRequest(result);
        //    }
        //    return Ok(result);
        //}
    }
}
