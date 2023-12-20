using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Dtos.Inventory;

namespace TailorProTrack.Api.Controllers
{
  
    public class InventoryController : Controller
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService service)
        {
            this._inventoryService = service;
        }
        // GET: InventoryController
        [HttpGet("GetInventory")]
        public IActionResult Get()
        {
            var result = this._inventoryService.GetAll();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("GetProduct")]
        public IActionResult Get(int id)
        {
            var result = this._inventoryService.GetById(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        // Inventory
        [HttpPost("AddInventory")]
        public ActionResult Add([FromBody] InventoryDtoAdd inventoryDtoAdd)
        {
            var result = this._inventoryService.Add(inventoryDtoAdd);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }
}
