using UnityEngine;
using BepInEx;
using HarmonyLib;

namespace DiceCallbackPlugin
{

    [BepInPlugin(Guid, "HolloFoxes' Dice Color Sync Plug-In", Version)]
    public class DiceCallbackPlugin : BaseUnityPlugin
    {
        // constants
        public const string Guid = "org.hollofox.plugins.DiceColorSyncPlugin";
        private const string Version = "1.0.0.0";

        /// <summary>
        /// Awake plugin
        /// </summary>
        void Awake()
        {
            Debug.Log("DiceColorSync Plug-in loaded");
            var harmony = new Harmony(Guid);
            harmony.PatchAll();
        }
    }
}
