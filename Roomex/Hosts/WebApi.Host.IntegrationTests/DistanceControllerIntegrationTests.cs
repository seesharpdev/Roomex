namespace WebApi.Host.IntegrationTests
{
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc.Testing;
	using System.Net;
	using System.Threading.Tasks;
	using Xunit;

	public class DistanceControllerIntegrationTests
		: IClassFixture<WebApplicationFactory<Startup>>
	{
		private readonly WebApplicationFactory<Startup> _factory;
		private readonly string baseUrl = "api/Distance";

		public DistanceControllerIntegrationTests(WebApplicationFactory<Startup> factory)
		{
			_factory = factory;
		}

		[Theory]
		[InlineData(53.297975, -6.372663, 41.385101, -81.440440, "K")]
		[InlineData(53.297975, -6.372663, 41.385101, -81.440440, "M")]
		public async Task GetDistance_WithValidInput_ReturnsTheDistance(
			double firstLatitude,
			double firstLongitude,
			double secondLatitude,
			double secondLongitude,
			string distanceUnit)
		{
			// Arrange
			var client = _factory.CreateClient();
			string requestUri = BuildRequestUrl(firstLatitude, firstLongitude, secondLatitude, secondLongitude, distanceUnit);

			// Act
			var response = await client.GetAsync(requestUri);

			// Assert
			//response.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
			var actual = await response.Content.ReadAsStringAsync();
			Assert.NotEqual("0", actual);
		}

		[Theory]
		[InlineData(53.297975, -6.372663, 41.385101, -81.440440, "L")]
		public async Task GetDistance_WithInvalidDistanceUnit_ReturnsTheError(
			double firstLatitude,
			double firstLongitude,
			double secondLatitude,
			double secondLongitude,
			string distanceUnit)
		{
			// Arrange
			var client = _factory.CreateClient();
			string requestUri = BuildRequestUrl(firstLatitude, firstLongitude, secondLatitude, secondLongitude, distanceUnit);

			// Act
			var response = await client.GetAsync(requestUri);

			// Assert
			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
			Assert.Equal("text/plain; charset=utf-8", response.Content.Headers.ContentType.ToString());
			var actual = await response.Content.ReadAsStringAsync();
			Assert.Equal("Unsupported distance unit.", actual);
		}

		private string BuildRequestUrl(double firstLatitude, double firstLongitude, double secondLatitude, double secondLongitude, string distanceUnit)
		{
			return $"{baseUrl}?firstLatitude={firstLatitude}&firstLongitude={firstLongitude}&secondLatitude={secondLatitude}&secondLongitude={secondLongitude}&distanceUnit={distanceUnit}";
		}
	}
}
