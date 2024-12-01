using BeatSaberMarkupLanguage.GameplaySetup;
using HarmonyLib;
using IPA;
using IPA.Config.Stores;
using System.Reflection;
using IPALogger = IPA.Logging.Logger;

namespace PitchBlack
{
	[Plugin(RuntimeOptions.DynamicInit)]
	public class Plugin
	{
		internal static Plugin Instance;
		internal static IPALogger Log;
		internal static Harmony harmony;

		[Init]
		public Plugin(IPALogger logger, IPA.Config.Config conf)
		{
			Instance = this;
			Log = logger;
			Config.Instance = conf.Generated<Config>();
			harmony = new Harmony("Loloppe.BeatSaber.PitchBlack");
			BeatSaberMarkupLanguage.Util.MainMenuAwaiter.MainMenuInitializing += MainMenuInit;
		}

		[OnEnable]
		public void OnEnable()
		{
			harmony.PatchAll(Assembly.GetExecutingAssembly());
		}

		public void MainMenuInit()
		{
            GameplaySetup.Instance.AddTab("PitchBlack", "PitchBlack.Views.settings.bsml", Config.Instance, MenuType.All);
        }

		[OnDisable]
		public void OnDisable()
		{
			harmony.UnpatchSelf();
            GameplaySetup.Instance.RemoveTab("PitchBlack");
        }
	}
}
