﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TailorProTrack.Api.Utils;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Sale;

namespace TailorProTrack.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class SaleController : Controller
    {
        private readonly ISaleService _SaleService;

        public SaleController(ISaleService SaleService)
        {
            _SaleService = SaleService;
        }

        [HttpGet("GetSales")]
        public IActionResult GetAll([FromQuery] PaginationParams @params)
        {
            var result = this._SaleService.GetAll(@params);
            ServiceResult response = new ServiceResult { Data = result.Data, Message = result.Message, Success = result.Success };

            if (!result.Success)
            {
                return BadRequest(response);
            }
            Response.AddHeaderPaginationJson(result.Header);
            return Ok(response);
        }

        [HttpGet("GetSale")]
        public IActionResult GetById(int id)
        {
            var result = this._SaleService.GetById(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("UpdateSale")]
        public IActionResult Update([FromBody] SaleDtoUpdate dtoUpdate)
        {
            var result = this._SaleService.Update(dtoUpdate);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("RemoveSale")]
        public IActionResult Remove([FromBody] SaleDtoRemove dtoRemove)
        {
            var result = this._SaleService.Remove(dtoRemove);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    } 
}
