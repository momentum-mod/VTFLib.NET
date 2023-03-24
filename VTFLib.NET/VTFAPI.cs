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
		[DllImport("VTFLib.dll", EntryPoint = "vlGetVersion")]
		public static extern uint GetVersion();

		[DllImport("VTFLib.dll", EntryPoint = "vlGetVersionString")]
		public static extern string GetVersionString();

		[DllImport("VTFLib.dll", EntryPoint = "vlGetLastError")]
		public static extern string GetLastError();

		[DllImport("VTFLib.dll", EntryPoint = "vlInitialize")]
		public static extern bool Initialize();

		[DllImport("VTFLib.dll", EntryPoint = "vlShutdown")]
		public static extern void Shutdown();

		[DllImport("VTFLib.dll", EntryPoint = "vlGetBoolean")]
		public static extern bool GetBoolean(VTFLibOption option);

		[DllImport("VTFLib.dll", EntryPoint = "vlSetBoolean")]
		public static extern void SetBoolean(VTFLibOption option, bool value);

		[DllImport("VTFLib.dll", EntryPoint = "vlGetInteger")]
		public static extern int GetInteger(VTFLibOption option);

		[DllImport("VTFLib.dll", EntryPoint = "vlSetInteger")]
		public static extern void SetInteger(VTFLibOption option, int value);

		[DllImport("VTFLib.dll", EntryPoint = "vlGetFloat")]
		public static extern float GetFloat(VTFLibOption option);

		[DllImport("VTFLib.dll", EntryPoint = "vlSetFloat")]
		public static extern void SetFloat(VTFLibOption option, float value);
	}
}