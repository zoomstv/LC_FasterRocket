using HarmonyLib;
using Unity.Netcode;

namespace FasterRocket.Patches
{
    [HarmonyPatch(typeof(ItemDropship))]
    internal class ItemDropshipPatch
    {
        public static ItemDropshipPatch Instance;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void UpdateShipTimer(ref float ___shipTimer, ref bool ___deliveringOrder, ItemDropship __instance)
        {
            if(((NetworkBehaviour)__instance).IsServer)
            {
                if (!___deliveringOrder)
                {
                    if (___shipTimer > 2f)
                    {
                        ___shipTimer = 50f;
                    }
                }
            }
        }
    }
}
