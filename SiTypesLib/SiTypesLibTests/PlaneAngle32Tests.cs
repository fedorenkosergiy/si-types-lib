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
		public void FromDegreesToTotalDegreesCheck(int degrees)
		{
			PlaneAngle32 angle = PlaneAngle32.FromDegrees(degrees);
			int postConvertedDegrees = angle.TotalDegrees;
			Assert.AreEqual(degrees, postConvertedDegrees);
		}

		[TestCase(360, 0)]
		[TestCase(-360, 0)]
		[TestCase(361, 1)]
		[TestCase(721, 1)]
		[TestCase(-1, 359)]
		[TestCase(-361, 359)]
		[TestCase(540, 180)]
		[TestCase(-540, 180)]
		public void FromDegreesOverOneTurnToTotalDegreesCheck(int degreesOverOneTurn, int expectedDegrees)
		{
			PlaneAngle32 angle = PlaneAngle32.FromDegrees(degreesOverOneTurn);
			int postConvertedDegrees = angle.TotalDegrees;
			Assert.AreEqual(expectedDegrees, postConvertedDegrees);
		}
	}
}
