namespace Roomex.Core.Interfaces.Services
{
	using Roomex.Core.Interfaces.Enums;
	using Roomex.Core.Interfaces.ValueObjects;

	public interface IGeoLocationService
	{
		double GetDistance(Coordinate firstCoordinate, Coordinate secondCoordinate, DistanceUnit distanceUnit);
	}
}