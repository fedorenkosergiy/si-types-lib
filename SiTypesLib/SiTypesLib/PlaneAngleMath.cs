namespace SiTypesLib
{
	public static class PlaneAngleMath
	{
		public const int DegreesPerTurn = 360;
		public const int ArcMinutesPerDegree = 60;
		public const int ArcSecondsPerArcMinute = 60;
		public const int ArcSecondsPerDegree = ArcMinutesPerDegree * ArcSecondsPerArcMinute;
		public const int ArcMinutesPerTurn = DegreesPerTurn * ArcMinutesPerDegree;
		public const int ArcSecondsPerTurn = ArcMinutesPerTurn * ArcSecondsPerArcMinute;
		public const int PiRadiansPerTurn = 2;
		public const int DegreesPerPiRadian = 180;
	}
}
