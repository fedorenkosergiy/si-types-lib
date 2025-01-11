using NUnit.Framework;
using System;

namespace SiTypesLib
{
	public class PlaneAngle31Tests
	{
		[TestCase(0)]
		[TestCase(1)]
		[TestCase(2)]
		[TestCase(100)]
		[TestCase(359)]
		public void FromDegreesToDegreesCheck(int degrees)
		{
			PlaneAngle31 angle = PlaneAngle31.FromDegrees(degrees);
			Assert.AreEqual(degrees, angle.Degrees);
		}

		[TestCase(360, 0)]
		[TestCase(-360, 0)]
		[TestCase(361, 1)]
		[TestCase(721, 1)]
		[TestCase(-1, 359)]
		[TestCase(-361, 359)]
		[TestCase(540, 180)]
		[TestCase(-540, 180)]
		public void FromDegreesOverOneTurnToDegreesCheck(int degreesOverOneTurn, int expected)
		{
			PlaneAngle31 angle = PlaneAngle31.FromDegrees(degreesOverOneTurn);
			Assert.AreEqual(expected, angle.Degrees);
		}

		[TestCase(61, 1)]
		[TestCase(121, 1)]
		[TestCase(-61, 59)]
		[TestCase(360, 0)]
		[TestCase(-360, 0)]
		[TestCase(361, 1)]
		[TestCase(721, 1)]
		[TestCase(-1, 59)]
		[TestCase(-361, 59)]
		[TestCase(540, 0)]
		[TestCase(-540, 0)]
		[TestCase(21_599, 59)]
		public void FromArcMinutesToArcMinutesCheck(int arcMinutes, int expected)
		{
			PlaneAngle31 angle = PlaneAngle31.FromArcMinutes(arcMinutes);
			Assert.AreEqual(expected, angle.ArcMinutes);
		}

		[TestCase(61, 1)]
		[TestCase(121, 2)]
		[TestCase(-61, 358)]
		[TestCase(360, 6)]
		[TestCase(-360, 354)]
		[TestCase(361, 6)]
		[TestCase(721, 12)]
		[TestCase(-1, 359)]
		[TestCase(-361, 353)]
		[TestCase(540, 9)]
		[TestCase(-540, 351)]
		[TestCase(21_599, 359)]
		[TestCase(21_600, 0)]
		[TestCase(21_660, 1)]
		public void FromArcMinutesToDegreesCheck(int arcMinutes, int expected)
		{
			PlaneAngle31 angle = PlaneAngle31.FromArcMinutes(arcMinutes);
			Assert.AreEqual(expected, angle.Degrees);
		}

		[TestCase(61, 1)]
		[TestCase(121, 2)]
		[TestCase(-61, 58)]
		[TestCase(360, 6)]
		[TestCase(-360, 54)]
		[TestCase(361, 6)]
		[TestCase(721, 12)]
		[TestCase(-1, 59)]
		[TestCase(-361, 53)]
		[TestCase(540, 9)]
		[TestCase(-540, 51)]
		public void FromArcSecondsToArcMinutesCheck(int arcSeconds, int expected)
		{
			PlaneAngle31 angle = PlaneAngle31.FromArcSeconds(arcSeconds);
			Assert.AreEqual(expected, angle.ArcMinutes);
		}

		[TestCase(61, 0)]
		[TestCase(12100, 3)]
		[TestCase(3600, 1)]
		[TestCase(-3600, 359)]
		public void FromArcSecondsToDegreesCheck(int arcSeconds, int expected)
		{
			PlaneAngle31 angle = PlaneAngle31.FromArcSeconds(arcSeconds);
			Assert.AreEqual(expected, angle.Degrees);
		}

		[TestCase(61, 1)]
		[TestCase(121, 1)]
		[TestCase(-61, 59)]
		[TestCase(360, 0)]
		[TestCase(-360, 0)]
		[TestCase(361, 1)]
		[TestCase(721, 1)]
		[TestCase(-1, 59)]
		[TestCase(-361, 59)]
		[TestCase(540, 0)]
		public void FromArcSecondsToArcSecondsCheck(int arcSeconds, int expected)
		{
			PlaneAngle31 angle = PlaneAngle31.FromArcSeconds(arcSeconds);
			Assert.AreEqual(expected, angle.ArcSeconds);
		}

		[TestCase(0, 0, 0)]
		[TestCase(0, 10, 10)]
		[TestCase(180, 180, 0)]
		[TestCase(180, 181, 1)]
		[TestCase(359, 359, 358)]
		[TestCase(359, -359, 0)]
		[TestCase(180, -180, 0)]
		public void Sum(int degreesA, int degreesB, int degreesExpected)
		{
			PlaneAngle31 a = PlaneAngle31.FromDegrees(degreesA);
			PlaneAngle31 b = PlaneAngle31.FromDegrees(degreesB);
			PlaneAngle31 expected = PlaneAngle31.FromDegrees(degreesExpected);
			PlaneAngle31 sum = a + b;
			PlaneAngle31 alsoSum = b + a;
			Assert.AreEqual(expected, sum);
			Assert.AreEqual(expected, alsoSum);
			Assert.Zero(sum.ArcMinutes);
			Assert.Zero(sum.ArcSeconds);
		}

		[TestCase(0, 0, 0)]
		[TestCase(10, 0, 10)]
		[TestCase(10, 2, 8)]
		[TestCase(2, 10, 352)]
		[TestCase(0, 180, 180)]
		public void Diff(int degreesA, int degreesB, int degreesExpected)
		{
			PlaneAngle31 a = PlaneAngle31.FromDegrees(degreesA);
			PlaneAngle31 b = PlaneAngle31.FromDegrees(degreesB);
			PlaneAngle31 expected = PlaneAngle31.FromDegrees(degreesExpected);
			PlaneAngle31 diff = a - b;
			Assert.AreEqual(expected, diff);
			Assert.Zero(diff.ArcMinutes);
			Assert.Zero(diff.ArcSeconds);
		}

		[TestCase(0, 0)]
		[TestCase(10, 350)]
		[TestCase(180, 180)]
		public void Negation(int degrees, int degreesExpected)
		{
			PlaneAngle31 actual = -PlaneAngle31.FromDegrees(degrees);
			PlaneAngle31 expected = PlaneAngle31.FromDegrees(degreesExpected);
			Assert.AreEqual(expected, actual);
			Assert.Zero(actual.ArcMinutes);
			Assert.Zero(actual.ArcSeconds);
		}

		[TestCase(0, 1, 0, 0, 0)]
		[TestCase(10, 2, 5, 0, 0)]
		[TestCase(10, 4, 2, 30, 0)]
		[TestCase(10, 8, 1, 15, 0)]
		[TestCase(10, 16, 0, 37, 30)]
		[TestCase(10, 32, 0, 18, 45)]
		[TestCase(10, 64, 0, 9, 22)]
		[TestCase(10, 10, 1, 0, 0)]
		public void Division(int degreesA, int b, int degreesExpected, int minutesExpected, int secondsExpected)
		{
			PlaneAngle31 a = PlaneAngle31.FromDegrees(degreesA);
			PlaneAngle31 div = a / b;
			Assert.AreEqual(degreesExpected, div.Degrees);
			Assert.AreEqual(minutesExpected, div.ArcMinutes);
			Assert.AreEqual(secondsExpected, div.ArcSeconds);
		}

		[TestCase(0, 1, 0)]
		[TestCase(10, 2, 5)]
		[TestCase(10, 2, 5)]
		[TestCase(10, 4, 2)]
		[TestCase(10, 8, 1)]
		[TestCase(10, 10, 1)]
		public void Division(int degreesA, int degreesB, int expected)
		{
			PlaneAngle31 a = PlaneAngle31.FromDegrees(degreesA);
			PlaneAngle31 b = PlaneAngle31.FromDegrees(degreesB);
			int div = a / b;
			Assert.AreEqual(expected, div);
		}

		[Test]
		[TestCase(10, 0)]
		public void DivisionByNotPositiveLeadsToException([Random(0, 359, 10)] int degreesA,
			[Random(int.MinValue, 0, 10)] int notPositiveB)
		{
			PlaneAngle31 a = PlaneAngle31.FromDegrees(degreesA);
			ArgumentException? e = Assert.Throws<ArgumentException>(() => _ = a / notPositiveB);
			Assert.AreEqual("b", e?.ParamName);
		}
		
		[Test]
		public void DivideByZero()
		{
			PlaneAngle31 a = PlaneAngle31.FromDegrees(10);
			PlaneAngle31 b = PlaneAngle31.FromDegrees(0);
			DivideByZeroException? e = Assert.Throws<DivideByZeroException>(() => _ = a / b);
		}
		
		[TestCase(0, 0, 0 )]
		[TestCase(0, 1, 0 )]
		[TestCase(10, 2, 20)]
		[TestCase(359, 2, 358 )]
		[TestCase(359, 359, 1 )]
		[TestCase(359, 360, 0 )]
		[TestCase(123, 360, 0 )]
		[TestCase(100, 180, 0 )]
		[TestCase(5, 90, 90 )]
		[TestCase(5, -90, 270 )]
		public void Multiplication(int degreesA, int b, int degreesExpected)
		{
			PlaneAngle31 a = PlaneAngle31.FromDegrees(degreesA);
			PlaneAngle31 expected = PlaneAngle31.FromDegrees(degreesExpected);
			PlaneAngle31 multiplication = a * b;
			PlaneAngle31 alsoMultiplication =  b * a;
			Assert.AreEqual(expected, multiplication);
			Assert.AreEqual(expected, alsoMultiplication);
		}
	}
}
