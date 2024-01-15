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
        static void UpdateShipTimer(ref float ___shipTimer, ref bool ___deliveringOrder, ref StartOfRound ___playersManager, ref Terminal ___terminalScript, ItemDropship __instance)
        {
            float timeToLandRocket = 10f;

            // ship will land 40 seconds after ordering something.
            // after (timeToLandRocket) seconds the timer will be set to 41 seconds so the ship lands earlier.

            if (((NetworkBehaviour)__instance).IsServer)
            {
                if (!___deliveringOrder)
                {
                    if (___terminalScript.orderedItemsFromTerminal.Count > 0)
                    {
                        if (___playersManager.shipHasLanded)
                        {
                            if (___shipTimer > timeToLandRocket && ___shipTimer < 40f)
                            {
                                ___shipTimer = 41f;
                            }
                        }
                    }
                }
            }
        }

        [HarmonyPatch("ShipLeave")]
        [HarmonyPrefix]
        static void ResetShipTimer(ref float ___shipTimer)
        {
            ___shipTimer = 0f;

            // resets the shiptimer after the ship has left.
            // idk why the game doesn't do this by itself.
        }
    }
}
