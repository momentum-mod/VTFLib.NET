using VTFLib;

const string fileName = @"E:\MomentumDev\game\platform\materials\tools\toolsclip.vtf";

uint image = 0;

VTFAPI.vlInitialize();

VTFFile.vlCreateImage(ref image);
VTFFile.vlBindImage(image);
VTFFile.vlImageLoad(fileName, false);

Console.WriteLine($"Version: {VTFFile.vlImageGetMajorVersion()}.{VTFFile.vlImageGetMinorVersion()}");
Console.WriteLine($"Width: {VTFFile.vlImageGetWidth()}");
Console.WriteLine($"Height: {VTFFile.vlImageGetHeight()}");

//Console.WriteLine("Version: " + VTFUtil.vlGetVersion());
//Console.WriteLine("Init: " + VTFUtil.vlInitialize());
//Console.WriteLine("Image created: " + VTFUtil.vlCreateImage(ref uiImage));
//Console.WriteLine("Image bound: " + VTFUtil.vlBindImage(uiImage));
//Console.WriteLine("Image loaded: " + VTFUtil.vlImageLoad(fileName, false));
//Console.WriteLine($"Version: {VTFUtil.vlImageGetMajorVersion()}.{VTFUtil.vlImageGetMinorVersion()}");
