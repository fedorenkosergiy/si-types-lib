using System.Diagnostics;
using System.Globalization;
using static SiTypesLib.PlaneAngleMath;

namespace SiTypesLib
{
	[DebuggerDisplay("{Degrees}° {ArcMinutes}' {ArcSeconds}\"")]
	public struct PlaneAngle31
	{
		private const int OneArcSecondRaw = int.MaxValue / ArcSecondsPerTurn;
		private const int OneArcMinuteRaw = OneArcSecondRaw * ArcSecondsPerArcMinute;
		private const int OneDegreeRaw = OneArcSecondRaw * ArcSecondsPerDegree;
		private const int OneTurnRaw = OneDegreeRaw * DegreesPerTurn;
		private const double AccuracyInRadians = Math.PI * PiRadiansPerTurn / OneTurnRaw;

		private int _raw;

		public int Degrees => _raw / OneDegreeRaw;

		public int ArcMinutes => _raw / OneArcMinuteRaw % ArcMinutesPerDegree;

		public int ArcSeconds => _raw / OneArcSecondRaw % ArcSecondsPerArcMinute;

		public double Turns => (double)_raw / OneTurnRaw;

		public double Radians => _raw * AccuracyInRadians;

		public static PlaneAngle31 Zero => default;

		public static PlaneAngle31 Pi { get; } = new() { _raw = DegreesPerPiRadian * OneDegreeRaw };

		public static PlaneAngle31 FromDegrees(int value)
		{
			int degrees = FilterOutExtraTurnsFromDegrees(value);
			return new PlaneAngle31 { _raw = degrees * OneDegreeRaw };
		}

		public static PlaneAngle31 FromArcMinutes(int value)
		{
			int arcMinutes = FilterOutExtraTurnsFromArcMinutes(value);
			return new PlaneAngle31 { _raw = arcMinutes * OneArcMinuteRaw };
		}

		public static PlaneAngle31 FromArcSeconds(int value)
		{
			int arcSeconds = FilterOutExtraTurnsFromArcSeconds(value);
			return new PlaneAngle31 { _raw = arcSeconds * OneArcSecondRaw };
		}

		public override string ToString() => ToString(AngleStringFormat.RadiansDecimal);

		public string ToString(AngleStringFormat format)
		{
			return format switch
			{
				AngleStringFormat.RadiansDecimal => Radians.ToString(CultureInfo.InvariantCulture),
				AngleStringFormat.RadiansOverPi => (Radians / Math.PI).ToString(CultureInfo.InvariantCulture) + " π",
				AngleStringFormat.DegreesDecimal =>
					((double)_raw / OneDegreeRaw).ToString(CultureInfo.InvariantCulture),
				AngleStringFormat.DegreesInteger => Degrees.ToString(),
				AngleStringFormat.DegreesMinutesSeconds => $"{Degrees}° {ArcMinutes}' {ArcSeconds}\"",
				_ => throw new NotSupportedException($"Not supported format {format}")
			};
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

		private static int FilterOutExtraTurnsFromArcSeconds(int arcSeconds)
		{
			int lessThenATurn = arcSeconds % ArcSecondsPerTurn;
			return lessThenATurn < 0 ? lessThenATurn + ArcSecondsPerTurn : lessThenATurn;
		}

		public static PlaneAngle31 operator +(PlaneAngle31 a, PlaneAngle31 b)
		{
			int raw = a._raw - OneTurnRaw + b._raw;
			if (raw < 0) raw += OneTurnRaw;

			return new PlaneAngle31 { _raw = raw % OneTurnRaw };
		}

		public static PlaneAngle31 operator -(PlaneAngle31 a, PlaneAngle31 b)
		{
			int raw = a._raw - b._raw;
			if (raw < 0) raw += OneTurnRaw;

			return new PlaneAngle31 { _raw = raw };
		}

		public static PlaneAngle31 operator -(PlaneAngle31 angle)
		{
			int raw = -angle._raw + OneTurnRaw;

			return new PlaneAngle31 { _raw = raw % OneTurnRaw };
		}

		public static int operator /(PlaneAngle31 a, PlaneAngle31 b) => a._raw / b._raw;

		public static PlaneAngle31 operator /(PlaneAngle31 a, int b)
		{
			const string msg = "An angle can not be divided by a non positive number";
			if (b <= 0) throw new ArgumentException(msg, nameof(b));
			return new PlaneAngle31 { _raw = a._raw / b };
		}

		public static PlaneAngle31 operator *(PlaneAngle31 a, int b)
		{
			long raw = a._raw * (long)b;
			raw %= OneTurnRaw;
			if (raw < 0) raw += OneTurnRaw;

			return new PlaneAngle31 { _raw = (int)raw };
		}

		public static PlaneAngle31 operator *(int a, PlaneAngle31 b) => b * a;

		public static implicit operator double(PlaneAngle31 angle) => angle.Radians;
	}
}
