using System.Numerics;

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
			Assert.That(VTFFile.vlImageGetDepth(), Is.EqualTo(1));
			Assert.That(VTFFile.vlImageGetFrameCount(), Is.EqualTo(1));
			Assert.That(VTFFile.vlImageGetStartFrame(), Is.EqualTo(0));
			Assert.That(VTFFile.vlImageGetFaceCount(), Is.EqualTo(1));
			Assert.That(VTFFile.vlImageGetMipmapCount(), Is.EqualTo(9));
			Assert.That(VTFFile.vlImageGetFlags(), Is.EqualTo(0));
			Assert.That(VTFFile.vlImageGetBumpmapScale(), Is.EqualTo(1));

			var reflectivity = new Vector3();
			VTFFile.vlImageGetReflectivity(ref reflectivity.X, ref reflectivity.Y, ref reflectivity.Z);
			Assert.That(reflectivity.X, Is.EqualTo(0.5));
			Assert.That(reflectivity.Y, Is.EqualTo(0.5));
			Assert.That(reflectivity.Z, Is.EqualTo(0.5));
		}
	}
}