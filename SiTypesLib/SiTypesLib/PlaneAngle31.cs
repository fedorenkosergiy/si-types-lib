﻿using System.Diagnostics;
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

		private int _raw;

		public int Degrees => _raw / OneDegreeRaw;

		public int ArcMinutes => _raw / OneArcMinuteRaw % ArcMinutesPerDegree;

		public int ArcSeconds => _raw / OneArcSecondRaw % ArcSecondsPerArcMinute;

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

		public override string ToString()
		{
			return _raw.ToString();
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
			
			return new PlaneAngle31 { _raw = raw % OneTurnRaw};
		}
		
		public static int operator /(PlaneAngle31 a, PlaneAngle31 b) => a._raw / b._raw;
		public static PlaneAngle31 operator /(PlaneAngle31 a, int b)
		{
			const string msg = "An angle can not be divided by a non positive number";
			if (b <= 0) throw new ArgumentException(msg, nameof(b));
			return new PlaneAngle31 { _raw = a._raw / b };
		}
	}
}
