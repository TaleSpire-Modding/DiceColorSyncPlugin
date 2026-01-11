using UnityEngine;
using BepInEx;
using HarmonyLib;
using PluginUtilities;

namespace DiceColorSyncPlugin
{

    [BepInPlugin(Guid, Name, Version)]
    [BepInDependency(SetInjectionFlag.Guid)]
    public class DiceColorSyncPlugin : DependencyUnityPlugin<DiceColorSyncPlugin>
    {
        // constants
        public const string Name = "Dice Color Sync Plug-In";
        public const string Guid = "org.hollofox.plugins.DiceColorSyncPlugin";
        internal const string Version = "0.0.0.0";

        Harmony harmony;

        /// <summary>
        /// Awake plugin
        /// </summary>
        protected override void OnAwake()
        {
            Debug.Log("DiceColorSync Plug-in loaded");
            harmony = new Harmony(Guid);
            harmony.PatchAll();
        }

        protected override void OnDestroyed()
        {
            harmony?.UnpatchSelf();
        }
    }
}
