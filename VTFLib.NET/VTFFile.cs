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

	public static class VTFFile
	{
#if WINDOWS
	const string dllName = "VTFLib.dll";
#elif LINUX
	const string dllName = "libvtflib.so";
#endif

		[DllImport(dllName, EntryPoint = "vlImageIsBound")]
		public static extern bool ImageIsBound();

		[DllImport(dllName, EntryPoint = "vlBindImage")]
		public static extern bool BindImage(uint image);

		[DllImport(dllName, EntryPoint = "vlCreateImage")]
		public static extern bool CreateImage(ref uint image);

		[DllImport(dllName, EntryPoint = "vlDeleteImage")]
		public static extern void DeleteImage(uint image);

		[DllImport(dllName, EntryPoint = "vlImageCreateDefaultCreateStructure")]
		public static extern void ImageCreateDefaultCreateStructure(ref SVTFCreateOptions vtfCreateOptions);

		[DllImport(dllName, EntryPoint = "vlImageCreate")]
		public static extern bool ImageCreate(uint width, uint height, uint frames, uint faces, uint slices, VTFImageFormat imageFormat, bool thumbnail, bool mipMaps, bool nullImageData);

		[DllImport(dllName, EntryPoint = "vlImageCreateSingle")]
		public static extern bool ImageCreateSingle(uint width, uint height, byte[] imageDataRGBA8888, ref SVTFCreateOptions vtfCreateOptions);

		[DllImport(dllName, EntryPoint = "vlImageCreateMultiple")]
		public static extern bool ImageCreateMultiple(uint width, uint height, uint frames, uint faces, uint slices, byte[] imageDataRGBA8888, ref SVTFCreateOptions vtfCreateOptions);

		[DllImport(dllName, EntryPoint = "vlImageDestroy")]
		public static extern void ImageDestroy();

		[DllImport(dllName, EntryPoint = "vlImageIsLoaded")]
		public static extern bool ImageIsLoaded();

		[DllImport(dllName, EntryPoint = "vlImageLoad")]
		public static extern bool ImageLoad(string fileName, bool headerOnly);

		[DllImport(dllName, EntryPoint = "vlImageLoadLump")]
		public static extern bool ImageLoadLump(byte[] data, uint bufferSize, bool headerOnly);

		[DllImport(dllName, EntryPoint = "vlImageLoadProc")]
		public static extern bool ImageLoadProc(byte[] userData, bool headerOnly);

		[DllImport(dllName, EntryPoint = "vlImageSave")]
		public static extern bool ImageSave(string fileName);

		[DllImport(dllName, EntryPoint = "vlImageSaveLump")]
		public static extern bool ImageSaveLump(byte[] data, uint bufferSize, ref uint size);

		[DllImport(dllName, EntryPoint = "vlImageSaveProc")]
		public static extern bool ImageSaveProc(byte[] userData);

		[DllImport(dllName, EntryPoint = "vlImageGetHasImage")]
		public static extern uint ImageGetHasImage();

		[DllImport(dllName, EntryPoint = "vlImageGetMajorVersion")]
		public static extern uint ImageGetMajorVersion();

		[DllImport(dllName, EntryPoint = "vlImageGetMinorVersion")]
		public static extern uint ImageGetMinorVersion();

		[DllImport(dllName, EntryPoint = "vlImageGetSize")]
		public static extern uint ImageGetSize();

		[DllImport(dllName, EntryPoint = "vlImageGetWidth")]
		public static extern uint ImageGetWidth();

		[DllImport(dllName, EntryPoint = "vlImageGetHeight")]
		public static extern uint ImageGetHeight();

		[DllImport(dllName, EntryPoint = "vlImageGetDepth")]
		public static extern uint ImageGetDepth();

		[DllImport(dllName, EntryPoint = "vlImageGetFrameCount")]
		public static extern uint ImageGetFrameCount();

		[DllImport(dllName, EntryPoint = "vlImageGetFaceCount")]
		public static extern uint ImageGetFaceCount();

		[DllImport(dllName, EntryPoint = "vlImageGetMipmapCount")]
		public static extern uint ImageGetMipmapCount();

		[DllImport(dllName, EntryPoint = "vlImageGetStartFrame")]
		public static extern uint ImageGetStartFrame();

		[DllImport(dllName, EntryPoint = "vlImageSetStartFrame")]
		public static extern void ImageSetStartFrame(uint startFrame);

		[DllImport(dllName, EntryPoint = "vlImageGetFlags")]
		public static extern uint ImageGetFlags();

		[DllImport(dllName, EntryPoint = "vlImageSetFlags")]
		public static extern void ImageSetFlags(uint flags);

		[DllImport(dllName, EntryPoint = "vlImageGetFlag")]
		public static extern bool ImageGetFlag(VTFImageFlag flag);

		[DllImport(dllName, EntryPoint = "vlImageSetFlag")]
		public static extern void ImageSetFlag(VTFImageFlag flag, bool value);

		[DllImport(dllName, EntryPoint = "vlImageGetBumpmapScale")]
		public static extern float ImageGetBumpmapScale();

		[DllImport(dllName, EntryPoint = "vlImageSetBumpmapScale")]
		public static extern void ImageSetBumpmapScale(float bumpmapScale);

		[DllImport(dllName, EntryPoint = "vlImageGetReflectivity")]
		public static extern void ImageGetReflectivity(ref float x, ref float y, ref float z);

		[DllImport(dllName, EntryPoint = "vlImageSetReflectivity")]
		public static extern void ImageSetReflectivity(float x, float y, float z);

		[DllImport(dllName, EntryPoint = "vlImageGetFormat")]
		public static extern VTFImageFormat ImageGetFormat();

		[DllImport(dllName, EntryPoint = "vlImageGetData")]
		public static extern IntPtr ImageGetData(uint frame, uint face, uint slice, uint mipmapLevel);

		[DllImport(dllName, EntryPoint = "vlImageSetData")]
		public static extern void ImageSetData(uint frame, uint face, uint slice, uint mipmapLevel, byte[] data);

		[DllImport(dllName, EntryPoint = "vlImageGetHasThumbnail")]
		public static extern bool ImageGetHasThumbnail();

		[DllImport(dllName, EntryPoint = "vlImageGetThumbnailWidth")]
		public static extern uint ImageGetThumbnailWidth();

		[DllImport(dllName, EntryPoint = "vlImageGetThumbnailHeight")]
		public static extern uint ImageGetThumbnailHeight();

		[DllImport(dllName, EntryPoint = "vlImageGetThumbnailFormat")]
		public static extern VTFImageFormat ImageGetThumbnailFormat();

		[DllImport(dllName, EntryPoint = "vlImageGetThumbnailData")]
		public static extern IntPtr ImageGetThumbnailData();

		[DllImport(dllName, EntryPoint = "vlImageSetThumbnailData")]
		public static extern void ImageSetThumbnailData(byte[] data);

		[DllImport(dllName, EntryPoint = "vlImageGetSupportsResources")]
		public static extern bool ImageGetSupportsResources();

		[DllImport(dllName, EntryPoint = "vlImageGetResourceCount")]
		public static extern uint ImageGetResourceCount();

		[DllImport(dllName, EntryPoint = "vlImageGetResourceType")]
		public static extern uint ImageGetResourceType(uint index);

		[DllImport(dllName, EntryPoint = "vlImageGetHasResource")]
		public static extern bool ImageGetHasResource(uint type);

		[DllImport(dllName, EntryPoint = "vlImageGetResourceData")]
		public static extern IntPtr ImageGetResourceData(uint type, ref uint size);

		[DllImport(dllName, EntryPoint = "vlImageSetResourceData")]
		public static extern IntPtr ImageSetResourceData(uint type, uint size, byte[] data);

		[DllImport(dllName, EntryPoint = "vlImageGenerateMipmaps")]
		public static extern bool ImageGenerateMipmaps(uint face, uint frame, VTFMipmapFilter mipmapFilter, bool srgb);

		[DllImport(dllName, EntryPoint = "vlImageGenerateAllMipmaps")]
		public static extern bool ImageGenerateAllMipmaps(VTFMipmapFilter mipmapFilter, bool srgb);

		[DllImport(dllName, EntryPoint = "vlImageGenerateThumbnail")]
		public static extern bool ImageGenerateThumbnail(bool srgb);

		[DllImport(dllName, EntryPoint = "vlImageGenerateNormalMap")]
		public static extern bool ImageGenerateNormalMap(uint frame, VTFKernelFilter kernelFilter, VTFHeightConversionMethod heightConversionMethod, VTFNormalAlphaResult normalAlphaResult);

		[DllImport(dllName, EntryPoint = "vlImageGenerateAllNormalMaps")]
		public static extern bool ImageGenerateAllNormalMaps(VTFKernelFilter kernelFilter, VTFHeightConversionMethod heightConversionMethod, VTFNormalAlphaResult normalAlphaResult);

		[DllImport(dllName, EntryPoint = "vlImageGenerateSphereMap")]
		public static extern bool ImageGenerateSphereMap();

		[DllImport(dllName, EntryPoint = "vlImageComputeReflectivity")]
		public static extern bool ImageComputeReflectivity();

		[DllImport(dllName, EntryPoint = "vlImageGetFormatInfo")]
		public static extern ref SVTFImageFormatInfo ImageGetFormatInfo(VTFImageFormat format);

		[DllImport(dllName, EntryPoint = "vlImageGetImageFormatInfoEx")]
		public static extern bool ImageGetImageFormatInfoEx(VTFImageFormat format, ref SVTFImageFormatInfo info);

		[DllImport(dllName, EntryPoint = "vlImageComputeImageSize")]
		public static extern uint ImageComputeImageSize(uint width, uint height, uint depth, uint mipmapCount, VTFImageFormat format);

		[DllImport(dllName, EntryPoint = "vlImageComputeMipmapCount")]
		public static extern uint ImageComputeMipmapCount(uint width, uint height, uint depth);

		[DllImport(dllName, EntryPoint = "vlImageComputeMipmapDimensions")]
		public static extern void ImageComputeMipmapDimensions(uint width, uint height, uint depth, uint mipmapLevel, ref uint mipmapWidth, ref uint mipmapHeight, ref uint mipmapDepth);

		[DllImport(dllName, EntryPoint = "vlImageComputeMipmapSize")]
		public static extern uint ImageComputeMipmapSize(uint width, uint height, uint depth, uint mipmapLevel, VTFImageFormat format);

		[DllImport(dllName, EntryPoint = "vlImageConvertToRGBA8888")]
		public static extern bool ImageConvertToRGBA8888(byte[] source, byte[] dest, uint width, uint height, VTFImageFormat sourceFormat);

		[DllImport(dllName, EntryPoint = "vlImageConvertFromRGBA8888")]
		public static extern bool ImageConvertFromRGBA8888(byte[] source, byte[] dest, uint width, uint height, VTFImageFormat destFormat);

		[DllImport(dllName, EntryPoint = "vlImageConvert")]
		public static extern bool ImageConvert(byte[] source, byte[] dest, uint width, uint height, VTFImageFormat sourceFormat, VTFImageFormat destFormat);

		[DllImport(dllName, EntryPoint = "vlImageResize")]
		public static extern bool ImageResize(byte[] sourceRGBA8888, byte[] destRGBA8888, uint sourceWidth, uint sourceHeight, uint destWidth, uint destHeight, VTFMipmapFilter resizeFilter, bool srgb);

		[DllImport(dllName, EntryPoint = "vlImageCorrectImageGamma")]
		public static extern void ImageCorrectImageGamma(byte[] imageRGBA8888, uint width, uint height, float gammaCorrection);

		[DllImport(dllName, EntryPoint = "vlImageComputeImageReflectivity")]
		public static extern void ImageComputeImageReflectivity(byte[] imageRGBA8888, uint width, uint height, ref float x, ref float y, ref float z);

		[DllImport(dllName, EntryPoint = "vlImageFlipImage")]
		public static extern void ImageFlipImage(byte[] imageRGBA8888, uint width, uint height);

		[DllImport(dllName, EntryPoint = "vlImageMirrorImage")]
		public static extern void ImageMirrorImage(byte[] imageRGBA8888, uint width, uint height);
	}
}
