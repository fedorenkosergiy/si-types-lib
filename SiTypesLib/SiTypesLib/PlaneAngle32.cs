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
			int totalSeconds = degrees * ArcSecondsPerDegree;
			int raw = totalSeconds * OneArcSecondRaw;
			return new PlaneAngle32 { _raw = raw };
		}
		
	}
}
