namespace Roomex.Core.Interfaces.ValueObjects
{
	public struct Coordinate
	{
		public double Latitude { get; }

		public double Longitude {  get; }

		public Coordinate(double latitude, double longitude)
		{
			Latitude = latitude;
			Longitude = longitude;
		}
	}
}
