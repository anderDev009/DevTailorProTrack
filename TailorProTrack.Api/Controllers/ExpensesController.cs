﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TailorProTrack.Api.Utils;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Expenses;
using TailorProTrack.Application.Dtos.Reports;

namespace TailorProTrack.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class ExpensesController : Controller
    {
        private readonly IExpensesService _expensesService;

        public ExpensesController(IExpensesService expensesService)
        {
            _expensesService = expensesService;
        }


        [HttpPost("SaveExpense")]
        public IActionResult Save([FromBody] ExpensesDtoAdd dtoAdd)
        {
            var result = _expensesService.Add(dtoAdd);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("GetExpenses")]
        public IActionResult Get([FromQuery] PaginationParams @params)
        {
            var result = this._expensesService.GetAll(@params);
            ServiceResult response = new ServiceResult { Data = result.Data, Message = result.Message, Success = result.Success };
            if (!response.Success)
            {
                return BadRequest(response);
            }
            Response.AddHeaderPaginationJson(result.Header);
            return Ok(response);
        }
        [HttpGet("GetBuysPending")]
        public IActionResult GetBuysPending()
        {
            var result = this._expensesService.GetBuysPending();
            ServiceResult response = new ServiceResult { Data = result.Data, Message = result.Message, Success = result.Success };
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("GetOnlyExpensesPending")]
        public IActionResult GetOnlyExpensesPending()
        {
            var result = this._expensesService.GetOnlyExpensesPending();
            ServiceResult response = new ServiceResult { Data = result.Data, Message = result.Message, Success = result.Success };
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("GetExpenseById")]
        public IActionResult GetById(int id)
        {
            var result = this._expensesService.GetById(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpGet("GetExpensesPending")]
        public IActionResult GetExpensesPending()
        {
            var result = this._expensesService.GetExpensesPending();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("GetExpensesPendingWithBuyPaginated")]
        public IActionResult GetExpensesWithBuyPaginated([FromQuery] PaginationParams @params)
        {
            var result = this._expensesService.GetExpensesWithIdBuyPaginated(@params);
            ServiceResult response = new ServiceResult { Data = result.Data, Message = result.Message, Success = result.Success };
            if (!response.Success)
            {
                return BadRequest(response);
            }
            Response.AddHeaderPaginationJson(result.Header);
            return Ok(response);
        }
        [HttpGet("GetExpensesPendingWithoutBuyPaginated")]
        public IActionResult GetExpensesWithoutBuyPaginated([FromQuery] PaginationParams @params)
        {
            var result = this._expensesService.GetExpensesWithoutBuyPaginated(@params);
            ServiceResult response = new ServiceResult { Data = result.Data, Message = result.Message, Success = result.Success };
            if (!response.Success)
            {
                return BadRequest(response);
            }
            Response.AddHeaderPaginationJson(result.Header);
            return Ok(response);
        }

        [HttpGet("GetExpensesPendingWithBuy")]
        public IActionResult GetExpensesWithBuyId()
        {
            var result = this._expensesService.GetExpensesWithIdBuy();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("GetExpensesPendingWithoutBuy")]
        public IActionResult GetExpensesWithoutBuyId()
        {
            var result = this._expensesService.GetExpensesWithoutBuy();
            if (!result.Success)
            {
                return StatusCode(500,result);
            }
            return Ok(result);
        }

        [HttpPut("UpdateExpenses")]
        public IActionResult UpdateById([FromBody] ExpensesDtoUpdate dtoUpdate)
        {
            var result = this._expensesService.Update(dtoUpdate,dtoUpdate.Id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("RemoveExpense")]
        public IActionResult Remove(int id)
        {
            var result = this._expensesService.Remove(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPatch("ConfirmExpenses")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ConfirmExpenses(int id)
        {
            var result = this._expensesService.ConfirmExpenses(id);
            if (!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,result.Message);
            }
            return NoContent();
        }

        [HttpPost("ReportExpenses")]
        public IActionResult ReportExpenses([FromBody] DateRangeDto dto)
        {
            var result = _expensesService.GetExpensesByDate(dto.start_date, dto.end_date);
            if (!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.Message);

            }
            return Ok(result);
        }
        [HttpPost("ReportBuys")]
        public IActionResult ReporBuys([FromBody] DateRangeDto dto)
        {
            var result = _expensesService.GetBuysByDate(dto.start_date, dto.end_date);
            if (!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.Message);

            }
            return Ok(result);
        }
    }
}
