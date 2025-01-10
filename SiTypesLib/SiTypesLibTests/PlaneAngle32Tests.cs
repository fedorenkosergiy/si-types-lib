using NUnit.Framework;

namespace SiTypesLib
{
	public class PlaneAngle32Tests
	{
		[TestCase(0)]
		[TestCase(1)]
		[TestCase(-1)]
		[TestCase(2)]
		[TestCase(100)]
		[TestCase(-100)]
		[TestCase(359)]
		[TestCase(-359)]
		public void FromDegreesToTotalDegreesCheck(int degrees)
		{
			PlaneAngle32 angle = PlaneAngle32.FromDegrees(degrees);
			int postConvertedDegrees = angle.TotalDegrees;
			Assert.AreEqual(degrees, postConvertedDegrees);
		}
	}
}
