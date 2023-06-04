using System.Runtime.InteropServices;
using System.Text;

namespace VTFLib
{
	public enum VTFLibOption
	{
		VTFLIB_LUMINANCE_WEIGHT_R,
		VTFLIB_LUMINANCE_WEIGHT_G,
		VTFLIB_LUMINANCE_WEIGHT_B,

		VTFLIB_BLUESCREEN_MASK_R,
		VTFLIB_BLUESCREEN_MASK_G,
		VTFLIB_BLUESCREEN_MASK_B,

		VTFLIB_BLUESCREEN_CLEAR_R,
		VTFLIB_BLUESCREEN_CLEAR_G,
		VTFLIB_BLUESCREEN_CLEAR_B,

		VTFLIB_FP16_HDR_EXPOSURE,

		VTFLIB_VMT_PARSE_MODE
	}

	public static class VTFAPI
	{
#if WINDOWS
	const string dllName = "VTFLib.dll";
#elif LINUX
	const string dllName = "libvtflib.so";
#endif

		[DllImport(dllName, EntryPoint = "vlGetVersion")]
		public static extern uint GetVersion();

		[DllImport(dllName, EntryPoint = "vlGetVersionString")]
		public static extern string GetVersionString();

		[DllImport(dllName, EntryPoint = "vlGetLastError")]
		public static extern string GetLastError();

		[DllImport(dllName, EntryPoint = "vlInitialize")]
		public static extern bool Initialize();

		[DllImport(dllName, EntryPoint = "vlShutdown")]
		public static extern void Shutdown();

		[DllImport(dllName, EntryPoint = "vlGetBoolean")]
		public static extern bool GetBoolean(VTFLibOption option);

		[DllImport(dllName, EntryPoint = "vlSetBoolean")]
		public static extern void SetBoolean(VTFLibOption option, bool value);

		[DllImport(dllName, EntryPoint = "vlGetInteger")]
		public static extern int GetInteger(VTFLibOption option);

		[DllImport(dllName, EntryPoint = "vlSetInteger")]
		public static extern void SetInteger(VTFLibOption option, int value);

		[DllImport(dllName, EntryPoint = "vlGetFloat")]
		public static extern float GetFloat(VTFLibOption option);

		[DllImport(dllName, EntryPoint = "vlSetFloat")]
		public static extern void SetFloat(VTFLibOption option, float value);
	}
}
