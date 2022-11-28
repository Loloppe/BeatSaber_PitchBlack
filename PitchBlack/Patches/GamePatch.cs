using HarmonyLib;

namespace PitchBlack.Patches
{
    [HarmonyPatch(typeof(BeatmapData), nameof(BeatmapData.InsertBeatmapEventData))]

	static class SkipEvent
    {
		static bool Prefix(ref BeatmapEventData beatmapEventData)
		{
			if(Config.Instance.Enabled && Config.Instance.Light)
            {
				return false;
			}

			return true;
		}
	}

	[HarmonyPatch(typeof(LightSwitchEventEffect), nameof(LightSwitchEventEffect.Start))]

	static class PitchBlack
	{
		static bool Prefix()
		{
			if (Config.Instance.Enabled && Config.Instance.Light)
			{
				return false;
			}

			return true;
		}
	}


	[HarmonyPatch(typeof(StaticEnvironmentLights), nameof(StaticEnvironmentLights.Awake))]

	static class RemoveStatic
	{
		static bool Prefix()
		{
			if (Config.Instance.Enabled && Config.Instance.Light)
			{
				return false;
			}

			return true;
		}
	}

	[HarmonyPatch(typeof(Spectrogram), nameof(Spectrogram.Update))]

	static class RemoveSpectrogram
	{
		static bool Prefix()
		{
			if (Config.Instance.Enabled && Config.Instance.Spectrogram)
			{
				return false;
			}

			return true;
		}
	}
}
