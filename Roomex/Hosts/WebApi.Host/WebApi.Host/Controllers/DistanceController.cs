namespace WebApi.Host.Controllers
{
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;
	using Roomex.Core.Interfaces.Enums;
	using Roomex.Core.Interfaces.Services;
	using Roomex.Core.Interfaces.ValueObjects;
	using System;

	/// <summary>
	/// The <c>Distance</c> Controller.
	/// </summary>
	[ApiController]
	[Route("api/[controller]")]
	[ApiVersion("1.0")]
	[ApiVersion("1.1", Deprecated = true)]
	[ApiVersion("2.0")]
	public class DistanceController : ControllerBase
	{
		private readonly IGeoLocationService geoLocationService;
		private readonly ILogger<DistanceController> _logger;

		/// <summary>
		/// Initializes a new instance of the <c>DistanceController</c>.
		/// </summary>
		/// <param name="geoLocationService"></param>
		/// <param name="logger"></param>
		public DistanceController(IGeoLocationService geoLocationService, ILogger<DistanceController> logger)
		{
			this.geoLocationService = geoLocationService;
			_logger = logger;
		}

		/// <summary>
		/// Returns the distance between two coordinates.
		/// </summary>
		/// <param name="firstLatitude">The latitude of the first coordinate.</param>
		/// <param name="firstLongitude">The longitude of the first coordinate.</param>
		/// <param name="secondLatitude">The latitude of the second coordinate.</param>
		/// <param name="secondLongitude">The longitude of the second coordinate.</param>
		/// <param name="distanceUnit">The unit for the distance, either 'K' for kilometers or 'M' for miles.</param>
		/// <returns>The distance between the coordinates in the given distance unit.</returns>
		[HttpGet]
		[MapToApiVersion("1.0")]
		public IActionResult Get(double firstLatitude, double firstLongitude, double secondLatitude, double secondLongitude, string distanceUnit)
		{
			try
			{
				Coordinate firstCoordinate = new(firstLatitude, firstLongitude);
				Coordinate secondCoordinate = new(secondLatitude, secondLongitude);
				DistanceUnit unit;
				switch (distanceUnit.ToUpper())
				{
					case "K":
						unit = DistanceUnit.Kilometers;
						break;
					case "M":
						unit = DistanceUnit.Miles;
						break;
					default:
						return BadRequest("Unsupported distance unit.");
				}

				var distance = this.geoLocationService.GetDistance(firstCoordinate, secondCoordinate, unit);

				return Ok(distance);
			}
			catch (Exception exception)
			{
				_logger.LogError(exception.Message);
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}
	}
}
