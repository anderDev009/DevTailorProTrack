using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TailorProTrack.Api.Utils;
using TailorProTrack.Application.Contracts.BuyInventoryContracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.BuyInventoryDtos;

namespace TailorProTrack.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class BuyInventoryController : Controller
    {
        private readonly IBuyInventoryService _buyInventoryService;
        public BuyInventoryController(IBuyInventoryService buyInventoryService)
        {
            _buyInventoryService = buyInventoryService;
        }
      
        [HttpGet("GetBuys")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll([FromQuery]PaginationParams @params)
        {
            ServiceResultWithHeader result = this._buyInventoryService.GetAll(@params);

            ServiceResult response = new ServiceResult { Data = result.Data, Message = result.Message, Success = result.Success };

            if (!result.Success)
            {
                return BadRequest(response);
            }
            Response.AddHeaderPaginationJson(result.Header);
            return Ok(response);
        }

        [HttpGet("GetBuyById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetById(int id)
        {
            var result = this._buyInventoryService.GetById(id);

            ServiceResult response = new ServiceResult { Data = result.Data, Message = result.Message, Success = result.Success };

            if (!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,result.Message);
            }
            return Ok(response);
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
