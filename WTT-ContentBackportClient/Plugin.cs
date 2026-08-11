using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using UnityEngine;
using WTTContentBackportClient.Patches;

namespace WTTContentBackportClient
{
    [BepInPlugin("com.wtt.contentbackport", "WTT-ContentBackportClient", "1.1.4")]
    public class Plugin : BaseUnityPlugin
    {
        internal static RuntimeAnimatorController MannequinController { get; private set; }
        internal static Plugin Instance { get; private set; }
        internal static ManualLogSource Log { get; private set; }
        private void Awake()
        {
            Instance = this;
            Log = Logger;
            new SetMaxUniqueAnimationModIdPatch().Enable();
            new MannequinPatch().Enable();
            new EnvironmentUIAwakePatch().Enable();
            new EnvironmentUISetEnvironmentPatch().Enable();
            if (!LoadBundle())
            {
                Log.LogError("Bundle setup failed.");
            }
        }

        private bool LoadBundle()
        {
            var pluginFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var bundlePath = Path.Combine(pluginFolder!, "mannequin_animator_controller.bundle");

            var bundle = AssetBundle.LoadFromFile(bundlePath);
            if (bundle == null)
            {
                Log.LogError($"Failed to load bundle: {bundlePath}");
                return false;
            }

            MannequinController = bundle.LoadAsset<RuntimeAnimatorController>(
                "Assets/hideout/Mannequin Poses/Mannequin_animator_controller.controller");

            if (MannequinController == null)
            {
                Log.LogError("Failed to load mannequin controller.");
                bundle.Unload(false);
                return false;
            }
            DontDestroyOnLoad(MannequinController);
            return true;
        }
    }
}