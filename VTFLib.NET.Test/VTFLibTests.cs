namespace VTFLib.NET.Test
{
	public class VTFLibTests
	{
		[SetUp]
		public void Setup()
		{
			VTFAPI.vlInitialize();
		}

		[Test]
		public void ValidateVTFInfo()
		{
			const string fileName = "checkerboard.vtf";
			uint image = 0;
			
			VTFFile.vlCreateImage(ref image);
			VTFFile.vlBindImage(image);

			Assert.IsTrue(VTFFile.vlImageLoad(fileName, false));

			Assert.That(VTFFile.vlImageGetMajorVersion(), Is.EqualTo(7));
			Assert.That(VTFFile.vlImageGetMinorVersion(), Is.EqualTo(2));
			Assert.That(VTFFile.vlImageGetWidth(), Is.EqualTo(256));
			Assert.That(VTFFile.vlImageGetHeight(), Is.EqualTo(256));
		}
	}
}