using System.Numerics;

namespace VTFLib.NET.Test
{
	public class VTFLibTests
	{
		[SetUp]
		public void Setup()
		{
			VTFAPI.Initialize();
		}

		[Test]
		public void ValidateVTFInfo()
		{
			const string fileName = "checkerboard.vtf";
			uint image = 0;
			
			VTFFile.CreateImage(ref image);
			VTFFile.BindImage(image);

			Assert.IsTrue(VTFFile.ImageLoad(fileName, false));

			Assert.That(VTFFile.ImageGetMajorVersion(), Is.EqualTo(7));
			Assert.That(VTFFile.ImageGetMinorVersion(), Is.EqualTo(2));
			Assert.That(VTFFile.ImageGetWidth(), Is.EqualTo(256));
			Assert.That(VTFFile.ImageGetHeight(), Is.EqualTo(256));
			Assert.That(VTFFile.ImageGetDepth(), Is.EqualTo(1));
			Assert.That(VTFFile.ImageGetFrameCount(), Is.EqualTo(1));
			Assert.That(VTFFile.ImageGetStartFrame(), Is.EqualTo(0));
			Assert.That(VTFFile.ImageGetFaceCount(), Is.EqualTo(1));
			Assert.That(VTFFile.ImageGetMipmapCount(), Is.EqualTo(9));
			Assert.That(VTFFile.ImageGetFlags(), Is.EqualTo(0));
			Assert.That(VTFFile.ImageGetBumpmapScale(), Is.EqualTo(1));

			var reflectivity = new Vector3();
			VTFFile.ImageGetReflectivity(ref reflectivity.X, ref reflectivity.Y, ref reflectivity.Z);
			Assert.That(reflectivity.X, Is.EqualTo(0.5));
			Assert.That(reflectivity.Y, Is.EqualTo(0.5));
			Assert.That(reflectivity.Z, Is.EqualTo(0.5));
		}
	}
}