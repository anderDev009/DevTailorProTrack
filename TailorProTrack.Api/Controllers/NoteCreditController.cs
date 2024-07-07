using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TailorProTrack.Api.Utils;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;

namespace TailorProTrack.Api.Controllers
{

	[ApiController]
	[Route("api/[controller]")]
	[EnableCors("AllowSpecificOrigin")]
	public class NoteCreditController(INoteCreditService noteCreditService) : Controller
	{


		[HttpGet("GetAll")]
		public IActionResult Index([FromQuery] PaginationParams @params)
		{
			ServiceResultWithHeader result = noteCreditService.GetAll(@params);

			ServiceResult response = new ServiceResult { Data = result.Data, Message = result.Message, Success = result.Success };

			if (!result.Success)
			{
				return BadRequest(response);
			}
			Response.AddHeaderPaginationJson(result.Header);
			return Ok(response);
		}

		[HttpGet("GetById")]
		public IActionResult GetById(int id)
		{
			var result = noteCreditService.GetById(id);
			if (!result.Success)
			{
				return StatusCode(500,result);
			}
			return Ok(result);
		}

		[HttpGet("GetNotesByIdClient")]
		public IActionResult GetNotesByIdClient(int id)
		{
			var result = noteCreditService.SearchNoteCreditByClientId(id);
			if (!result.Success)
			{
				return StatusCode(500, result);
			}
			return Ok(result);
		}
	}
}
