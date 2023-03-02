using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VTFLib
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct SVTFImageFormatInfo
	{
		string name;					// Enumeration text equivalent.
		uint bitsPerPixel;				// Format bits per pixel.
		uint bytesPerPixel;				// Format bytes per pixel.
		uint redBitsPerPixel;			// Format red bits per pixel.  0 for N/A.
		uint greenBitsPerPixel;			// Format green bits per pixel.  0 for N/A.
		uint blueBitsPerPixel;			// Format blue bits per pixel.  0 for N/A.
		uint alphaBitsPerPixel;			// Format alpha bits per pixel.  0 for N/A.
		[MarshalAs(UnmanagedType.I1)]
		public bool isCompressed;		// Format is compressed (DXT).
		[MarshalAs(UnmanagedType.I1)]
		public bool isSupported;		// Format is supported by VTFLib.
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct SVTFCreateOptions
	{
		public uint versionMajor;									// Output image version.
		public uint versionMinor;									// Output image version.
		public VTFImageFormat imageFormat;							// Output image output storage format.

		public uint flags;											// Output image header flags.
		public uint startFrame;										// Output image start frame.
		public float bumpScale;										// Output image bump scale.
		public float reflectivityR;									// Output image reflectivity. (Only used if bReflectivity is false.)
		public float reflectivityG;									// Output image reflectivity. (Only used if bReflectivity is false.)
		public float reflectivityB;									// Output image reflectivity. (Only used if bReflectivity is false.)

		[MarshalAs(UnmanagedType.I1)]
		public bool mipmaps;										// Generate MIPmaps. (Space is always allocated.)
		public VTFMipmapFilter mipmapFilter;						// MIP map re-size filter.
		public VTFSharpenFilter mipmapSharpenFilter;				// MIP map sharpen filter.

		[MarshalAs(UnmanagedType.I1)]
		public bool thumbnail;										// Generate thumbnail image.
		[MarshalAs(UnmanagedType.I1)]
		public bool reflectivity;									// Compute image reflectivity.

		[MarshalAs(UnmanagedType.I1)]
		public bool resize;											// Resize the input image.
		public VTFResizeMethod resizeMethod;						// New size compution method.
		public VTFMipmapFilter resizeFilter;						// Re-size filter.
		public VTFSharpenFilter resizeSharpenFilter;				// Sharpen filter.
		public uint resizeWidth;									// New width after re-size if method is RESIZE_SET.
		public uint resizeHeight;									// New height after re-size if method is RESIZE_SET.

		[MarshalAs(UnmanagedType.I1)]
		public bool resizeClamp;									// Clamp re-size size.
		public uint resizeClampWidth;								// Maximum width to re-size to.
		public uint resizeClampHeight;								// Maximum height to re-size to.

		[MarshalAs(UnmanagedType.I1)]
		public bool gammaCorrectionEnabled;							// Gamma correct input image.
		public float gammaCorrectionAmount;							// Gamma correction to apply.

		[MarshalAs(UnmanagedType.I1)]
		public bool normalMap;										// Convert input image to a normal map.
		public VTFKernelFilter kernelFilter;						// Normal map generation kernel.
		public VTFHeightConversionMethod heightConversionMethod;	// Method or determining height from input image during normal map creation.
		public VTFNormalAlphaResult normalAlphaResult;				// How to handle output image alpha channel, post normal map creation.
		public byte normalMinimumZ;									// Minimum normal Z value.
		public float normalScale;									// Normal map scale.
		[MarshalAs(UnmanagedType.I1)]
		public bool normalWrap;										// Wrap the normal map.
		[MarshalAs(UnmanagedType.I1)]
		public bool normalInvertX;									// Invert the normal X component.
		[MarshalAs(UnmanagedType.I1)]
		public bool normalInvertY;									// Invert the normal Y component.
		[MarshalAs(UnmanagedType.I1)]
		public bool normalInvertZ;									// Invert the normal Z component.

		[MarshalAs(UnmanagedType.I1)]
		public bool sphereMap;										// Generate a sphere map for six faced environment maps.
	}

	public class VTFFile
	{
		[DllImport("VTFLib.dll")]
		public static extern bool vlImageIsBound();

		[DllImport("VTFLib.dll")]
		public static extern bool vlBindImage(uint image);

		[DllImport("VTFLib.dll")]
		public static extern bool vlCreateImage(ref uint image);

		[DllImport("VTFLib.dll")]
		public static extern void vlDeleteImage(uint image);

		[DllImport("VTFLib.dll")]
		public static extern void vlImageCreateDefaultCreateStructure(ref SVTFCreateOptions vtfCreateOptions);

		[DllImport("VTFLib.dll")]
		public static extern bool vlImageCreate(uint width, uint height, uint frames, uint faces, uint slices, VTFImageFormat imageFormat, bool thumbnail, bool mipMaps, bool nullImageData);

		[DllImport("VTFLib.dll")]
		public static extern bool vlImageCreateSingle(uint width, uint height, byte[] imageDataRGBA8888, ref SVTFCreateOptions vtfCreateOptions);

		[DllImport("VTFLib.dll")]
		public static extern bool vlImageCreateMultiple(uint width, uint height, uint frames, uint faces, uint slices, byte[] imageDataRGBA8888, ref SVTFCreateOptions vtfCreateOptions);

		[DllImport("VTFLib.dll")]
		public static extern void vlImageDestroy();

		[DllImport("VTFLib.dll")]
		public static extern bool vlImageIsLoaded();

		[DllImport("VTFLib.dll")]
		public static extern bool vlImageLoad(string fileName, bool headerOnly);

		[DllImport("VTFLib.dll")]
		public static extern bool vlImageLoadLump(byte[] data, uint bufferSize, bool headerOnly);

		[DllImport("VTFLib.dll")]
		public static extern bool vlImageLoadProc(byte[] userData, bool headerOnly);

		[DllImport("VTFLib.dll")]
		public static extern bool vlImageSave(string fileName);

		[DllImport("VTFLib.dll")]
		public static extern bool vlImageSaveLump(byte[] data, uint bufferSize, ref uint size);

		[DllImport("VTFLib.dll")]
		public static extern bool vlImageSaveProc(byte[] userData);

		[DllImport("VTFLib.dll")]
		public static extern uint vlImageGetHasImage();

		[DllImport("VTFLib.dll")]
		public static extern uint vlImageGetMajorVersion();

		[DllImport("VTFLib.dll")]
		public static extern uint vlImageGetMinorVersion();

		[DllImport("VTFLib.dll")]
		public static extern uint vlImageGetSize();

		[DllImport("VTFLib.dll")]
		public static extern uint vlImageGetWidth();

		[DllImport("VTFLib.dll")]
		public static extern uint vlImageGetHeight();

		[DllImport("VTFLib.dll")]
		public static extern uint vlImageGetDepth();

		[DllImport("VTFLib.dll")]
		public static extern uint vlImageGetFrameCount();

		[DllImport("VTFLib.dll")]
		public static extern uint vlImageGetFaceCount();

		[DllImport("VTFLib.dll")]
		public static extern uint vlImageGetMipmapCount();

		[DllImport("VTFLib.dll")]
		public static extern uint vlImageGetStartFrame();

		[DllImport("VTFLib.dll")]
		public static extern void vlImageSetStartFrame(uint startFrame);

		[DllImport("VTFLib.dll")]
		public static extern uint vlImageGetFlags();

		[DllImport("VTFLib.dll")]
		public static extern void vlImageSetFlags(uint flags);

		[DllImport("VTFLib.dll")]
		public static extern bool vlImageGetFlag(VTFImageFlag flag);

		[DllImport("VTFLib.dll")]
		public static extern void vlImageSetFlag(VTFImageFlag flag, bool value);

		[DllImport("VTFLib.dll")]
		public static extern float vlImageGetBumpmapScale();

		[DllImport("VTFLib.dll")]
		public static extern void vlImageSetBumpmapScale(float bumpmapScale);

		[DllImport("VTFLib.dll")]
		public static extern void vlImageGetReflectivity(ref float x, ref float y, ref float z);

		[DllImport("VTFLib.dll")]
		public static extern void vlImageSetReflectivity(float x, float y, float z);

		[DllImport("VTFLib.dll")]
		public static extern VTFImageFormat vlImageGetFormat();

		[DllImport("VTFLib.dll")]
		public static extern byte[] vlImageGetData(uint frame, uint face, uint slice, uint mipmapLevel);

		[DllImport("VTFLib.dll")]
		public static extern void vlImageSetData(uint frame, uint face, uint slice, uint mipmapLevel, byte[] data);

		[DllImport("VTFLib.dll")]
		public static extern bool vlImageGetHasThumbnail();

		[DllImport("VTFLib.dll")]
		public static extern uint vlImageGetThumbnailWidth();

		[DllImport("VTFLib.dll")]
		public static extern uint vlImageGetThumbnailHeight();

		[DllImport("VTFLib.dll")]
		public static extern VTFImageFormat vlImageGetThumbnailFormat();

		[DllImport("VTFLib.dll")]
		public static extern byte[] vlImageGetThumbnailData();

		[DllImport("VTFLib.dll")]
		public static extern void vlImageSetThumbnailData(byte[] data);

		[DllImport("VTFLib.dll")]
		public static extern bool vlImageGetSupportsResources();

		[DllImport("VTFLib.dll")]
		public static extern uint vlImageGetResourceCount();

		[DllImport("VTFLib.dll")]
		public static extern uint vlImageGetResourceType(uint index);

		[DllImport("VTFLib.dll")]
		public static extern bool vlImageGetHasResource(uint type);

		[DllImport("VTFLib.dll")]
		public static extern byte[] vlImageGetResourceData(uint type, ref uint size);

		[DllImport("VTFLib.dll")]
		public static extern byte[] vlImageSetResourceData(uint type, uint size, byte[] data);

		[DllImport("VTFLib.dll")]
		public static extern bool vlImageGenerateMipmaps(uint face, uint frame, VTFMipmapFilter mipmapFilter, bool srgb);

		[DllImport("VTFLib.dll")]
		public static extern bool vlImageGenerateAllMipmaps(VTFMipmapFilter mipmapFilter, bool srgb);

		[DllImport("VTFLib.dll")]
		public static extern bool vlImageGenerateThumbnail(bool srgb);

		[DllImport("VTFLib.dll")]
		public static extern bool vlImageGenerateNormalMap(uint frame, VTFKernelFilter kernelFilter, VTFHeightConversionMethod heightConversionMethod, VTFNormalAlphaResult normalAlphaResult);

		[DllImport("VTFLib.dll")]
		public static extern bool vlImageGenerateAllNormalMaps(VTFKernelFilter kernelFilter, VTFHeightConversionMethod heightConversionMethod, VTFNormalAlphaResult normalAlphaResult);

		[DllImport("VTFLib.dll")]
		public static extern bool vlImageGenerateSphereMap();

		[DllImport("VTFLib.dll")]
		public static extern bool vlImageComputeReflectivity();

		[DllImport("VTFLib.dll")]
		public static extern ref SVTFImageFormatInfo vlImageGetFormatInfo(VTFImageFormat format);

		[DllImport("VTFLib.dll")]
		public static extern bool vlImageGetImageFormatInfoEx(VTFImageFormat format, ref SVTFImageFormatInfo info);

		[DllImport("VTFLib.dll")]
		public static extern uint vlImageComputeImageSize(uint width, uint height, uint depth, uint mipmapCount, VTFImageFormat format);

		[DllImport("VTFLib.dll")]
		public static extern uint vlImageComputeMipmapCount(uint width, uint height, uint depth);

		[DllImport("VTFLib.dll")]
		public static extern void vlImageComputeMipmapDimensions(uint width, uint height, uint depth, uint mipmapLevel, ref uint mipmapWidth, ref uint mipmapHeight, ref uint mipmapDepth);

		[DllImport("VTFLib.dll")]
		public static extern uint vlImageComputeMipmapSize(uint width, uint height, uint depth, uint mipmapLevel, VTFImageFormat format);

		[DllImport("VTFLib.dll")]
		public static extern bool vlImageConvertToRGBA8888(byte[] source, byte[] dest, uint width, uint height, VTFImageFormat sourceFormat);

		[DllImport("VTFLib.dll")]
		public static extern bool vlImageConvertFromRGBA8888(byte[] source, byte[] dest, uint width, uint height, VTFImageFormat destFormat);

		[DllImport("VTFLib.dll")]
		public static extern bool vlImageConvert(byte[] source, byte[] dest, uint width, uint height, VTFImageFormat sourceFormat, VTFImageFormat destFormat);

		[DllImport("VTFLib.dll")]
		public static extern bool vlImageResize(byte[] sourceRGBA8888, byte[] destRGBA8888, uint sourceWidth, uint sourceHeight, uint destWidth, uint destHeight, VTFMipmapFilter resizeFilter, bool srgb);

		[DllImport("VTFLib.dll")]
		public static extern void vlImageCorrectImageGamma(byte[] imageRGBA8888, uint width, uint height, float gammaCorrection);

		[DllImport("VTFLib.dll")]
		public static extern void vlImageComputeImageReflectivity(byte[] imageRGBA8888, uint width, uint height, ref float x, ref float y, ref float z);

		[DllImport("VTFLib.dll")]
		public static extern void vlImageFlipImage(byte[] imageRGBA8888, uint width, uint height);

		[DllImport("VTFLib.dll")]
		public static extern void vlImageMirrorImage(byte[] imageRGBA8888, uint width, uint height);
	}
}
