using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace FasterRocket
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class PluginBase : BaseUnityPlugin
    {
        private const string modGUID = "zoomstv.FasterRocket";
        private const string modName = "FasterRocket";
        private const string modVersion = "1.0.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static PluginBase Instance;

        internal static ManualLogSource mls;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            mls.LogInfo(modName + " loaded");

            harmony.PatchAll();
        }
    }
}
