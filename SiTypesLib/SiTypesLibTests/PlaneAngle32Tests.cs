using NUnit.Framework;

namespace SiTypesLib
{
	public class PlaneAngle32Tests
	{
		[TestCase(0)]
		[TestCase(1)]
		[TestCase(2)]
		[TestCase(100)]
		[TestCase(359)]
		public void FromDegreesToDegreesCheck(int degrees)
		{
			PlaneAngle32 angle = PlaneAngle32.FromDegrees(degrees);
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
			PlaneAngle32 angle = PlaneAngle32.FromDegrees(degreesOverOneTurn);
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
		public void FromArcMinutesOverOneDegreeToArcMinutesCheck(int arcMinutes, int expected)
		{
			PlaneAngle32 angle = PlaneAngle32.FromArcMinutes(arcMinutes);
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
		public void FromArcMinutesOverOneDegreeToDegreesCheck(int arcMinutes, int expected)
		{
			PlaneAngle32 angle = PlaneAngle32.FromArcMinutes(arcMinutes);
			Assert.AreEqual(expected, angle.Degrees);
		}
	}
}
