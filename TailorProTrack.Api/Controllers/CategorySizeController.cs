using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TailorProTrack.Api.Utils;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.CategoryProd;

namespace TailorProTrack.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class CategorySizeController : Controller
    {
        private readonly ICategorySizeService _service;

        public CategorySizeController(ICategorySizeService service) 
        {
            _service = service;
        }
        [HttpGet("GetCategoriesSize")]
        public IActionResult Get([FromQuery] PaginationParams @params)
        {
            ServiceResultWithHeader result = this._service.GetAll(@params);

            ServiceResult response = new ServiceResult { Data = result.Data, Message = result.Message, Success = result.Success };

            if (!result.Success)
            {
                return BadRequest(response);
            }
            Response.AddHeaderPaginationJson(result.Header);
            return Ok(response);
        }

        [HttpGet("GetCategorySize")]
        public IActionResult GetById(int id)
        {
            var result = this._service.GetById(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("AddCategorySize")]
        public IActionResult post([FromBody] CategoryProdDtoAdd dtoAdd)
        {
            var result = this._service.Add(dtoAdd);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("UpdateCategorySize")]
        public IActionResult Update([FromBody] CategoryProdDtoUpdate dtoUpdate)
        {
            var result = this._service.Update(dtoUpdate);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("RemoveCategorySize")]
        public IActionResult DeleteById([FromBody] CategoryProdDtoRemove dtoRemove)
        {
            var result = this._service.Remove(dtoRemove);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
