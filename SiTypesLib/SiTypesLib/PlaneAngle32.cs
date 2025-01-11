using static SiTypesLib.PlaneAngleMath;

namespace SiTypesLib
{
	public struct PlaneAngle32
	{
		private const int OneArcSecondRaw = int.MaxValue / ArcSecondsPerTurn;
		private const int OneArcMinuteRaw = OneArcSecondRaw * ArcSecondsPerArcMinute;
		private const int OneDegreeRaw = OneArcSecondRaw * ArcSecondsPerDegree;

		private int _raw;

		public int Degrees => _raw / OneDegreeRaw;

		public int ArcMinutes => _raw / OneArcMinuteRaw % ArcMinutesPerDegree;

		public static PlaneAngle32 FromDegrees(int value)
		{
			int degrees = FilterOutExtraTurnsFromDegrees(value);
			return new PlaneAngle32 { _raw = degrees * OneDegreeRaw };
		}


		public static PlaneAngle32 FromArcMinutes(int value)
		{
			int arcMinutes = FilterOutExtraTurnsFromArcMinutes(value);
			return new PlaneAngle32 { _raw = arcMinutes * OneArcMinuteRaw };
		}

		private static int FilterOutExtraTurnsFromDegrees(int degrees)
		{
			int lessThenATurn = degrees % DegreesPerTurn;
			return lessThenATurn < 0 ? lessThenATurn + DegreesPerTurn : lessThenATurn;
		}

		private static int FilterOutExtraTurnsFromArcMinutes(int arcMinutes)
		{
			int lessThenATurn = arcMinutes % ArcMinutesPerTurn;
			return lessThenATurn < 0 ? lessThenATurn + ArcMinutesPerTurn : lessThenATurn;
		}
	}
}
