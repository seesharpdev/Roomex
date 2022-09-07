namespace Roomex.Core.Services
{
	using Roomex.Core.Interfaces.Enums;
	using Roomex.Core.Interfaces.Services;
	using Roomex.Core.Interfaces.ValueObjects;
	using System;

	public class GeoLocationService : IGeoLocationService
	{
		public double GetDistance(Coordinate firstCoordinate, Coordinate secondCoordinate, DistanceUnit distanceUnit)
		{
			double radius = (distanceUnit == DistanceUnit.Miles) ? 3960 : 6371;
			var lat = (secondCoordinate.Latitude - firstCoordinate.Latitude).ToRadians();
			var lng = (secondCoordinate.Longitude - firstCoordinate.Longitude).ToRadians();
			var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
				Math.Cos(firstCoordinate.Latitude.ToRadians()) * Math.Cos(secondCoordinate.Latitude.ToRadians()) *
				Math.Sin(lng / 2) * Math.Sin(lng / 2);

			var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));

			return radius * h2;
		}

		private static double ConvertToRadians(double angle)
		{
			return (Math.PI / 180) * angle;
		}
	}
}
