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
		[DllImport("VTFLib.dll", EntryPoint = "vlImageIsBound")]
		public static extern bool ImageIsBound();

		[DllImport("VTFLib.dll", EntryPoint = "vlBindImage")]
		public static extern bool BindImage(uint image);

		[DllImport("VTFLib.dll", EntryPoint = "vlCreateImage")]
		public static extern bool CreateImage(ref uint image);

		[DllImport("VTFLib.dll", EntryPoint = "vlDeleteImage")]
		public static extern void DeleteImage(uint image);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageCreateDefaultCreateStructure")]
		public static extern void ImageCreateDefaultCreateStructure(ref SVTFCreateOptions vtfCreateOptions);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageCreate")]
		public static extern bool ImageCreate(uint width, uint height, uint frames, uint faces, uint slices, VTFImageFormat imageFormat, bool thumbnail, bool mipMaps, bool nullImageData);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageCreateSingle")]
		public static extern bool ImageCreateSingle(uint width, uint height, byte[] imageDataRGBA8888, ref SVTFCreateOptions vtfCreateOptions);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageCreateMultiple")]
		public static extern bool ImageCreateMultiple(uint width, uint height, uint frames, uint faces, uint slices, byte[] imageDataRGBA8888, ref SVTFCreateOptions vtfCreateOptions);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageDestroy")]
		public static extern void ImageDestroy();

		[DllImport("VTFLib.dll", EntryPoint = "vlImageIsLoaded")]
		public static extern bool ImageIsLoaded();

		[DllImport("VTFLib.dll", EntryPoint = "vlImageLoad")]
		public static extern bool ImageLoad(string fileName, bool headerOnly);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageLoadLump")]
		public static extern bool ImageLoadLump(byte[] data, uint bufferSize, bool headerOnly);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageLoadProc")]
		public static extern bool ImageLoadProc(byte[] userData, bool headerOnly);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageSave")]
		public static extern bool ImageSave(string fileName);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageSaveLump")]
		public static extern bool ImageSaveLump(byte[] data, uint bufferSize, ref uint size);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageSaveProc")]
		public static extern bool ImageSaveProc(byte[] userData);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGetHasImage")]
		public static extern uint ImageGetHasImage();

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGetMajorVersion")]
		public static extern uint ImageGetMajorVersion();

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGetMinorVersion")]
		public static extern uint ImageGetMinorVersion();

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGetSize")]
		public static extern uint ImageGetSize();

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGetWidth")]
		public static extern uint ImageGetWidth();

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGetHeight")]
		public static extern uint ImageGetHeight();

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGetDepth")]
		public static extern uint ImageGetDepth();

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGetFrameCount")]
		public static extern uint ImageGetFrameCount();

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGetFaceCount")]
		public static extern uint ImageGetFaceCount();

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGetMipmapCount")]
		public static extern uint ImageGetMipmapCount();

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGetStartFrame")]
		public static extern uint ImageGetStartFrame();

		[DllImport("VTFLib.dll", EntryPoint = "vlImageSetStartFrame")]
		public static extern void ImageSetStartFrame(uint startFrame);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGetFlags")]
		public static extern uint ImageGetFlags();

		[DllImport("VTFLib.dll", EntryPoint = "vlImageSetFlags")]
		public static extern void ImageSetFlags(uint flags);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGetFlag")]
		public static extern bool ImageGetFlag(VTFImageFlag flag);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageSetFlag")]
		public static extern void ImageSetFlag(VTFImageFlag flag, bool value);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGetBumpmapScale")]
		public static extern float ImageGetBumpmapScale();

		[DllImport("VTFLib.dll", EntryPoint = "vlImageSetBumpmapScale")]
		public static extern void ImageSetBumpmapScale(float bumpmapScale);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGetReflectivity")]
		public static extern void ImageGetReflectivity(ref float x, ref float y, ref float z);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageSetReflectivity")]
		public static extern void ImageSetReflectivity(float x, float y, float z);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGetFormat")]
		public static extern VTFImageFormat ImageGetFormat();

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGetData")]
		public static extern byte[] ImageGetData(uint frame, uint face, uint slice, uint mipmapLevel);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageSetData")]
		public static extern void ImageSetData(uint frame, uint face, uint slice, uint mipmapLevel, byte[] data);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGetHasThumbnail")]
		public static extern bool ImageGetHasThumbnail();

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGetThumbnailWidth")]
		public static extern uint ImageGetThumbnailWidth();

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGetThumbnailHeight")]
		public static extern uint ImageGetThumbnailHeight();

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGetThumbnailFormat")]
		public static extern VTFImageFormat ImageGetThumbnailFormat();

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGetThumbnailData")]
		public static extern byte[] ImageGetThumbnailData();

		[DllImport("VTFLib.dll", EntryPoint = "vlImageSetThumbnailData")]
		public static extern void ImageSetThumbnailData(byte[] data);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGetSupportsResources")]
		public static extern bool ImageGetSupportsResources();

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGetResourceCount")]
		public static extern uint ImageGetResourceCount();

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGetResourceType")]
		public static extern uint ImageGetResourceType(uint index);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGetHasResource")]
		public static extern bool ImageGetHasResource(uint type);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGetResourceData")]
		public static extern byte[] ImageGetResourceData(uint type, ref uint size);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageSetResourceData")]
		public static extern byte[] ImageSetResourceData(uint type, uint size, byte[] data);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGenerateMipmaps")]
		public static extern bool ImageGenerateMipmaps(uint face, uint frame, VTFMipmapFilter mipmapFilter, bool srgb);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGenerateAllMipmaps")]
		public static extern bool ImageGenerateAllMipmaps(VTFMipmapFilter mipmapFilter, bool srgb);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGenerateThumbnail")]
		public static extern bool ImageGenerateThumbnail(bool srgb);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGenerateNormalMap")]
		public static extern bool ImageGenerateNormalMap(uint frame, VTFKernelFilter kernelFilter, VTFHeightConversionMethod heightConversionMethod, VTFNormalAlphaResult normalAlphaResult);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGenerateAllNormalMaps")]
		public static extern bool ImageGenerateAllNormalMaps(VTFKernelFilter kernelFilter, VTFHeightConversionMethod heightConversionMethod, VTFNormalAlphaResult normalAlphaResult);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGenerateSphereMap")]
		public static extern bool ImageGenerateSphereMap();

		[DllImport("VTFLib.dll", EntryPoint = "vlImageComputeReflectivity")]
		public static extern bool ImageComputeReflectivity();

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGetFormatInfo")]
		public static extern ref SVTFImageFormatInfo ImageGetFormatInfo(VTFImageFormat format);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageGetImageFormatInfoEx")]
		public static extern bool ImageGetImageFormatInfoEx(VTFImageFormat format, ref SVTFImageFormatInfo info);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageComputeImageSize")]
		public static extern uint ImageComputeImageSize(uint width, uint height, uint depth, uint mipmapCount, VTFImageFormat format);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageComputeMipmapCount")]
		public static extern uint ImageComputeMipmapCount(uint width, uint height, uint depth);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageComputeMipmapDimensions")]
		public static extern void ImageComputeMipmapDimensions(uint width, uint height, uint depth, uint mipmapLevel, ref uint mipmapWidth, ref uint mipmapHeight, ref uint mipmapDepth);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageComputeMipmapSize")]
		public static extern uint ImageComputeMipmapSize(uint width, uint height, uint depth, uint mipmapLevel, VTFImageFormat format);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageConvertToRGBA8888")]
		public static extern bool ImageConvertToRGBA8888(byte[] source, byte[] dest, uint width, uint height, VTFImageFormat sourceFormat);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageConvertFromRGBA8888")]
		public static extern bool ImageConvertFromRGBA8888(byte[] source, byte[] dest, uint width, uint height, VTFImageFormat destFormat);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageConvert")]
		public static extern bool ImageConvert(byte[] source, byte[] dest, uint width, uint height, VTFImageFormat sourceFormat, VTFImageFormat destFormat);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageResize")]
		public static extern bool ImageResize(byte[] sourceRGBA8888, byte[] destRGBA8888, uint sourceWidth, uint sourceHeight, uint destWidth, uint destHeight, VTFMipmapFilter resizeFilter, bool srgb);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageCorrectImageGamma")]
		public static extern void ImageCorrectImageGamma(byte[] imageRGBA8888, uint width, uint height, float gammaCorrection);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageComputeImageReflectivity")]
		public static extern void ImageComputeImageReflectivity(byte[] imageRGBA8888, uint width, uint height, ref float x, ref float y, ref float z);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageFlipImage")]
		public static extern void ImageFlipImage(byte[] imageRGBA8888, uint width, uint height);

		[DllImport("VTFLib.dll", EntryPoint = "vlImageMirrorImage")]
		public static extern void ImageMirrorImage(byte[] imageRGBA8888, uint width, uint height);
	}
}
