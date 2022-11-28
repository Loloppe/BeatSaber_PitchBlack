﻿using BeatSaberMarkupLanguage.GameplaySetup;
using BeatSaberMarkupLanguage.Settings;
using HarmonyLib;
using IPA;
using IPA.Config.Stores;
using System;
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

		static class BsmlWrapper
		{
			static readonly bool hasBsml = IPA.Loader.PluginManager.GetPluginFromId("BeatSaberMarkupLanguage") != null;
			
			public static void EnableUI()
			{
				try
                {
					void wrap() => GameplaySetup.instance.AddTab("PitchBlack", "PitchBlack.Views.settings.bsml", Config.Instance, MenuType.All);

					if (hasBsml)
					{
						wrap();
					}
				}
				catch(Exception e)
                {
					Log.Warn(e.Message);
                }
				
			}
			public static void DisableUI()
			{
				void wrap() => GameplaySetup.instance.RemoveTab("PitchBlack");

				if (hasBsml)
				{
					wrap();
				}
			}
		}

		[Init]
		public Plugin(IPALogger logger, IPA.Config.Config conf)
		{
			Instance = this;
			Log = logger;
			Config.Instance = conf.Generated<Config>();
			harmony = new Harmony("Loloppe.BeatSaber.PitchBlack");
		}

		[OnEnable]
		public void OnEnable()
		{
			harmony.PatchAll(Assembly.GetExecutingAssembly());
			BsmlWrapper.EnableUI();
		}

		[OnDisable]
		public void OnDisable()
		{
			harmony.UnpatchSelf();
			BsmlWrapper.DisableUI();
		}
	}
}
