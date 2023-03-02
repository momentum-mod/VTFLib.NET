using DevILSharp;
using System.Numerics;
using System.Runtime.InteropServices;
using VTFLib;

VTFAPI.vlInitialize();
IL.Init();

PrintVTFInfo(@"C:\Users\tyler\Documents\Code\VTFLib.NET\VTFLib.NET.Test\checkerboard.vtf");
PrintImageInfo(@"C:\Users\tyler\Documents\Code\BSPConvert\BSPConversionCmd\bin\Debug\net6.0\Q3Content\textures\base_wall\c_met7_2.jpg",
	@"C:\Users\tyler\Downloads\test.vtf");

void PrintVTFInfo(string vtfPath)
{
	Console.WriteLine($"Displaying info for {vtfPath}");

	uint image = 0;
	VTFFile.vlCreateImage(ref image);
	VTFFile.vlBindImage(image);

	if (!VTFFile.vlImageLoad(vtfPath, false))
	{
		Console.WriteLine($"Error loading VTF");
		return;
	}

	Console.WriteLine($"Version: {VTFFile.vlImageGetMajorVersion()}.{VTFFile.vlImageGetMinorVersion()}");
	Console.WriteLine($"Width: {VTFFile.vlImageGetWidth()}");
	Console.WriteLine($"Height: {VTFFile.vlImageGetHeight()}");
	Console.WriteLine($"Depth: {VTFFile.vlImageGetDepth()}");
	Console.WriteLine($"Frames: {VTFFile.vlImageGetFrameCount()}");
	Console.WriteLine($"Start Frame: {VTFFile.vlImageGetStartFrame()}");
	Console.WriteLine($"Faces: {VTFFile.vlImageGetFaceCount()}");
	Console.WriteLine($"Mipmaps: {VTFFile.vlImageGetMipmapCount()}");
	Console.WriteLine($"Flags: {VTFFile.vlImageGetFlags()}");
	Console.WriteLine($"Bumpmap Scale: {VTFFile.vlImageGetBumpmapScale()}");
	var reflectivity = new Vector3();
	VTFFile.vlImageGetReflectivity(ref reflectivity.X, ref reflectivity.Y, ref reflectivity.Z);
	Console.WriteLine($"Reflectivity: {reflectivity}");
	//Console.WriteLine($"Format: {VTFFile.vlImageGetImageFormatInfo(VTFFile.vlImageGetFormat())}");
	Console.WriteLine($"Resources: {VTFFile.vlImageGetResourceCount()}");

	Console.WriteLine();
}

void PrintImageInfo(string imagePath, string savePath)
{
	Console.WriteLine($"Displaying info for {imagePath}");

	IL.Enable(EnableCap.AbsoluteOrigin); // Filps images that are upside down (by format).
	IL.OriginFunc(OriginMode.UpperLeft);

	var image = IL.GenImage();
	IL.BindImage(image);

	if (!IL.LoadImage(imagePath))
	{
		Console.WriteLine($"Error loading image");
		return;
	}

	var width = IL.GetInteger(IntName.ImageWidth);
	var height = IL.GetInteger(IntName.ImageHeight);
	var bpp = IL.GetInteger(IntName.ImageBytesPerPixel);

	Console.WriteLine($"Width: {width}");
	Console.WriteLine($"Height: {height}");
	Console.WriteLine($"BPP: {bpp}");

	var createOptions = new SVTFCreateOptions();
	VTFFile.vlImageCreateDefaultCreateStructure(ref createOptions);
	createOptions.imageFormat = IL.GetInteger(IntName.ImageBytesPerPixel) == 4 ? VTFImageFormat.IMAGE_FORMAT_DXT5 : VTFImageFormat.IMAGE_FORMAT_DXT1;

	if (!IL.ConvertImage(ChannelFormat.RGBA, ChannelType.UnsignedByte))
	{
		Console.WriteLine("Error converting image");
		return;
	}

	var size = width * height * 4;
	var data = new byte[size];
	Marshal.Copy(IL.GetData(), data, 0, size);

	if (!VTFFile.vlImageCreateSingle((uint)width, (uint)height, data, ref createOptions))
	{
		Console.WriteLine($"Error creating VTF file");
		return;
	}

	if (!VTFFile.vlImageSave(savePath))
	{
		Console.WriteLine($"Error saving VTF file");
		return;
	}

	Console.WriteLine($"Saved VTF file: {savePath}");

	Console.WriteLine();
}