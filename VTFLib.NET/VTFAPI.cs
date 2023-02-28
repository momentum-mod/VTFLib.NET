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
		[DllImport("VTFLib.dll")]
		public static extern uint vlGetVersion();

		[DllImport("VTFLib.dll")]
		public static extern string vlGetVersionString();

		[DllImport("VTFLib.dll")]
		public static extern string vlGetLastError();

		[DllImport("VTFLib.dll")]
		public static extern bool vlInitialize();

		[DllImport("VTFLib.dll")]
		public static extern void vlShutdown();

		[DllImport("VTFLib.dll")]
		public static extern bool vlGetBoolean(VTFLibOption option);

		[DllImport("VTFLib.dll")]
		public static extern void vlSetBoolean(VTFLibOption option, bool value);

		[DllImport("VTFLib.dll")]
		public static extern int vlGetInteger(VTFLibOption option);

		[DllImport("VTFLib.dll")]
		public static extern void vlSetInteger(VTFLibOption option, int value);

		[DllImport("VTFLib.dll")]
		public static extern float vlGetFloat(VTFLibOption option);

		[DllImport("VTFLib.dll")]
		public static extern void vlSetFloat(VTFLibOption option, float value);
	}
}