using NUnit.Framework;

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
	}
}
