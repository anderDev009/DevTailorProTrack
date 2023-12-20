using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Dtos.Inventory;
using TailorProTrack.Application.Dtos.InventoryColor;

namespace TailorProTrack.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("*")]
    public class InventoryController : Controller
    {
        private readonly IInventoryService _inventoryService;
        //inventory color service
        private readonly IInventoryColorService _inventoryColorService;
        public InventoryController(IInventoryService service,
                                   IInventoryColorService inventoryColorService)
        {
            this._inventoryService = service;
            _inventoryColorService = inventoryColorService; 
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
        [HttpPost("AddExistenceToInventory")]
        public ActionResult Add([FromBody] InventoryColorDtoAdd inventoryDtoAdd)
        {
            var result = this._inventoryColorService.AddAndUpdateInventory(inventoryDtoAdd);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
