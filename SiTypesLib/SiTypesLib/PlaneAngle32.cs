using static SiTypesLib.PlaneAngleMath;

namespace SiTypesLib
{
	public struct PlaneAngle32
	{
		private const int OneArcSecondRaw = int.MaxValue / ArcSecondsPerTurn;
		private const int OneDegreeRaw = OneArcSecondRaw * ArcSecondsPerDegree;

		private int _raw;

		public int TotalDegrees => _raw / OneDegreeRaw;
		
		public static PlaneAngle32 FromDegrees(int degrees)
		{
			int lessThenATurn = FilterOutExtraTurnsFromDegrees(degrees);
			int totalSeconds = lessThenATurn * ArcSecondsPerDegree;
			int raw = totalSeconds * OneArcSecondRaw;
			return new PlaneAngle32 { _raw = raw };
		}

		private static int FilterOutExtraTurnsFromDegrees(int degrees)
		{
			int lessThenATurn = degrees % DegreesPerTurn;
			return lessThenATurn < 0 ? lessThenATurn + DegreesPerTurn : lessThenATurn;
		}
	}
}
