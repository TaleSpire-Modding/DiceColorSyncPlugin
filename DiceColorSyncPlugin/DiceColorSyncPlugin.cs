using UnityEngine;
using BepInEx;
using HarmonyLib;
using ModdingTales;

namespace DiceColorSyncPlugin
{

    [BepInPlugin(Guid, Name, Version)]
    [BepInDependency(PluginUtilities.SetInjectionFlag.Guid)]
    public class DiceColorSyncPlugin : BaseUnityPlugin
    {
        // constants
        public const string Name = "Dice Color Sync Plug-In";
        public const string Guid = "org.hollofox.plugins.DiceColorSyncPlugin";
        internal const string Version = "0.0.0.0";

        /// <summary>
        /// Awake plugin
        /// </summary>
        void Awake()
        {
            Debug.Log("DiceColorSync Plug-in loaded");
            var harmony = new Harmony(Guid);
            harmony.PatchAll();

            ModdingUtils.AddPluginToMenuList(this);
        }
    }
}
