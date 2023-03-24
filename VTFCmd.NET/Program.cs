using DevILSharp;
using System.Numerics;
using System.Runtime.InteropServices;
using VTFLib;

VTFAPI.Initialize();
IL.Init();

PrintVTFInfo(@"C:\Users\tyler\Documents\Code\VTFLib.NET\VTFLib.NET.Test\checkerboard.vtf");
PrintImageInfo(@"C:\Users\tyler\Documents\Code\BSPConvert\BSPConversionCmd\bin\Debug\net6.0\Q3Content\textures\base_wall\c_met7_2.jpg",
	@"C:\Users\tyler\Downloads\test.vtf");

void PrintVTFInfo(string vtfPath)
{
	Console.WriteLine($"Displaying info for {vtfPath}");

	uint image = 0;
	VTFFile.CreateImage(ref image);
	VTFFile.BindImage(image);

	if (!VTFFile.ImageLoad(vtfPath, false))
	{
		Console.WriteLine($"Error loading VTF");
		return;
	}

	Console.WriteLine($"Version: {VTFFile.ImageGetMajorVersion()}.{VTFFile.ImageGetMinorVersion()}");
	Console.WriteLine($"Width: {VTFFile.ImageGetWidth()}");
	Console.WriteLine($"Height: {VTFFile.ImageGetHeight()}");
	Console.WriteLine($"Depth: {VTFFile.ImageGetDepth()}");
	Console.WriteLine($"Frames: {VTFFile.ImageGetFrameCount()}");
	Console.WriteLine($"Start Frame: {VTFFile.ImageGetStartFrame()}");
	Console.WriteLine($"Faces: {VTFFile.ImageGetFaceCount()}");
	Console.WriteLine($"Mipmaps: {VTFFile.ImageGetMipmapCount()}");
	Console.WriteLine($"Flags: {VTFFile.ImageGetFlags()}");
	Console.WriteLine($"Bumpmap Scale: {VTFFile.ImageGetBumpmapScale()}");
	var reflectivity = new Vector3();
	VTFFile.ImageGetReflectivity(ref reflectivity.X, ref reflectivity.Y, ref reflectivity.Z);
	Console.WriteLine($"Reflectivity: {reflectivity}");
	//Console.WriteLine($"Format: {VTFFile.ImageGetImageFormatInfo(VTFFile.ImageGetFormat())}");
	Console.WriteLine($"Resources: {VTFFile.ImageGetResourceCount()}");

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
	VTFFile.ImageCreateDefaultCreateStructure(ref createOptions);
	createOptions.imageFormat = IL.GetInteger(IntName.ImageBytesPerPixel) == 4 ? VTFImageFormat.IMAGE_FORMAT_DXT5 : VTFImageFormat.IMAGE_FORMAT_DXT1;

	if (!IL.ConvertImage(ChannelFormat.RGBA, ChannelType.UnsignedByte))
	{
		Console.WriteLine("Error converting image");
		return;
	}

	var size = width * height * 4;
	var data = new byte[size];
	Marshal.Copy(IL.GetData(), data, 0, size);

	if (!VTFFile.ImageCreateSingle((uint)width, (uint)height, data, ref createOptions))
	{
		Console.WriteLine($"Error creating VTF file");
		return;
	}

	if (!VTFFile.ImageSave(savePath))
	{
		Console.WriteLine($"Error saving VTF file");
		return;
	}

	Console.WriteLine($"Saved VTF file: {savePath}");

	Console.WriteLine();
}