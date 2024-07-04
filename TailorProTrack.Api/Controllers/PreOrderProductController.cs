using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Dtos.PreOrderProducts;

namespace TailorProTrack.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[EnableCors("AllowSpecificOrigin")]
	public class PreOrderProductController : Controller
	{
		private readonly IPreOrderProductService _preOrderProductService;

		public PreOrderProductController(IPreOrderProductService preOrderProductService)
		{
			_preOrderProductService = preOrderProductService;
		}


		[HttpPost("SavePreOrderProduct")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult Add([FromBody] PreOrderProductsDtoAdd dtoAdd, [FromQuery] int fkPreOrder)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return StatusCode(400, "Formato invalido");
				}

				_preOrderProductService.AddWithFkPreOrder(dtoAdd,fkPreOrder);
				return StatusCode(201);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

		}

		[HttpPut("UpdatePreOrderProduct")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult Update([FromBody] PreOrderProductsDtoUpdate dtoUpdate)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return StatusCode(400, "Formato invalido");
				}

				_preOrderProductService.Update(dtoUpdate);
				return StatusCode(201);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

		}

		[HttpDelete("RemovePreOrderProduct")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public IActionResult Remove([FromBody] PreOrderProductsDtoRemove dtoRemove)
		{
			try
			{
				_preOrderProductService.Remove(dtoRemove);
				return StatusCode(204);
			}
			catch (Exception e)
			{
				return StatusCode(500,e.Message);
			}
		}
	}
}
