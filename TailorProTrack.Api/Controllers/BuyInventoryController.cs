using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TailorProTrack.Application.Contracts.BuyInventoryContracts;
using TailorProTrack.Application.Dtos.BuyInventoryDtos;

namespace TailorProTrack.Api.Controllers
{
    public class BuyInventoryController : Controller
    {
        private readonly IBuyInventoryService _buyInventoryService;
        public BuyInventoryController(IBuyInventoryService buyInventoryService)
        {
            _buyInventoryService = buyInventoryService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("AddBuy")]
        public IActionResult AddBuy([FromBody] BuyInventoryDtoAdd dtoAdd)
        {
            var result = _buyInventoryService.Add(dtoAdd);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Created();

        }

        [HttpPut("UpdateOnlyBuyInfo")]
        public IActionResult UpdateInfoBuy([FromBody] BuyInventoryDtoUpdate dtoUpdate)
        {
            var result = _buyInventoryService.Update(dtoUpdate);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete("RemoveBuy")]
        public IActionResult RemoveBuy([FromBody] BuyInventoryDtoRemove dtoRemove)
        {
            var result = _buyInventoryService.Remove(dtoRemove);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
