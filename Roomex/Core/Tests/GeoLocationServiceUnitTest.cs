namespace GeoLocationService.UnitTests
{
	using Roomex.Core.Interfaces.Enums;
	using Roomex.Core.Interfaces.Services;
	using Roomex.Core.Interfaces.ValueObjects;
	using Roomex.Core.Services;
	using Xunit;

	public class GeoLocationServiceUnitTest
	{
		private static readonly Coordinate FirstCoordinate = new(53.297975, -6.372663);
		private static readonly Coordinate SecondCoordinate = new(41.385101, -81.440440);

		[Theory()]
		[InlineData(DistanceUnit.Kilometers)]
		[InlineData(DistanceUnit.Miles)]
		public void CalculateDistance_WhenCoordinatesAreEqual_ReturnsZero(DistanceUnit distanceUnit)
		{
			// Arrange
			double expected = 0;
			IGeoLocationService sut = new GeoLocationService();

			// Act
			var actual = sut.GetDistance(FirstCoordinate, FirstCoordinate, distanceUnit);

			// Assert
			Assert.Equal(expected, actual);
		}

		[Theory()]
		[InlineData(DistanceUnit.Kilometers)]
		[InlineData(DistanceUnit.Miles)]
		public void CalculateDistance_WhenCoordinatesAreNotEqual_ReturnsTheDistance(DistanceUnit distanceUnit)
		{
			// Arrange
			double expected = 0;
			IGeoLocationService sut = new GeoLocationService();

			// Act
			var actual = sut.GetDistance(FirstCoordinate, SecondCoordinate, distanceUnit);

			// Assert
			Assert.NotEqual(expected, actual);
		}
	}
}
