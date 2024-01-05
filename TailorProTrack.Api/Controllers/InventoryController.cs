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
    [EnableCors("AllowSpecificOrigin")]
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


        [HttpGet("GetInventoryById")]
        public IActionResult GetInventory(int id)
        {
            var result = this._inventoryService.GetInventoryById(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("GetProductByProductId")]
        public IActionResult Get(int id)
        {
            var result = this._inventoryService.GetById(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("GetInventoryColorByIdInventory")]
        public IActionResult GetInventoryColor(int id)
        {
            var result = this._inventoryColorService.GetByIdInventory(id);
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

        //[HttpGet("GetInventoryColorDetailsByIdInventory")]
        //public ActionResult GetDetails(int id)
        //{
        //    var result = this._inventoryColorService.GetById(id);
        //    if (!result.Success)
        //    {
        //        return BadRequest(result);
        //    }
        //    return Ok(result);
        //}

        [HttpPut("UpdateInventory")]
        public ActionResult Update([FromBody] InventoryDtoUpdate inventoryDtoUpdate)
        {
            var result = this._inventoryService.Update(inventoryDtoUpdate);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("UpdateInventoryColor")]
        public ActionResult UpdateInventoryColor([FromBody] InventoryColorDtoUpdate inventoryColorDtoUpdate)
        {
            var result = this._inventoryColorService.Update(inventoryColorDtoUpdate);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("RemoveInventory")]
        public ActionResult RemoveInventory([FromBody] InventoryDtoRemove inventoryDtoRemove)
        {
            var result = this._inventoryService.Remove(inventoryDtoRemove);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("RemoveInventoryColor")]
        public ActionResult RemoveInventoryColor([FromBody] InventoryColorDtoRemove inventoryColorDtoRemove)
        {
            var result = this._inventoryColorService.Remove(inventoryColorDtoRemove);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
