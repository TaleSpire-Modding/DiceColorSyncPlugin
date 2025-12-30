using HarmonyLib;
using UnityEngine;
using Dice;
using DataModel;
using static Dice.DiceRollManager;
using System.Collections.Generic;

namespace DiceColorSyncPlugin.Patches
{
    [HarmonyPatch(typeof(DiceRollManager), nameof(DiceRollManager.OnOp), typeof(RegisterRollOp), typeof(MessageInfo))]
    public class DiceRollManagerPatch
    {
        internal static Dictionary<RollId, ClientGuid> registeredDice = new Dictionary<RollId, ClientGuid>();
        static void Prefix(RegisterRollOp op, MessageInfo msgInfo)
        {
            registeredDice[op.RollId] = op.InitialDriveId.ClientId;
        }
    }

    [HarmonyPatch(typeof(Die), "Init")]
    public class DicePatch2
    {
        internal static System.Reflection.BindingFlags flag = System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance;
        static void Postfix(Die __instance, RollId rollId, Die.Mode mode, bool isGmOnly, MaterialPropertyBlock iconMpb, int faceIndexBase, ref Renderer ____dieRenderer)
        {
            //Debug.Log($"Die Init intercepted for RollId: {rollId}");
            if (!DiceRollManagerPatch.registeredDice.ContainsKey(rollId))
                return;

            if (TempClientColorManager.TryGetColor(DiceRollManagerPatch.registeredDice[rollId], out var color))
                ____dieRenderer.material.SetColor("_Color", color);
        }
    }

    [HarmonyPatch(typeof(Die), "SetMaterial")]
    public class SetMaterialPatch
    {
        static bool Prefix(ref Renderer ____dieRenderer, ref bool gmDie, Material ____normalMaterial, Material ____gmMaterial)
        {
            if (gmDie)
            {
                if (!((UnityEngine.Object)____dieRenderer.sharedMaterial != (UnityEngine.Object)____gmMaterial))
                    return false;

                var color = ____dieRenderer.material.GetColor("_Color");
                ____dieRenderer.sharedMaterial = ____gmMaterial;
                ____dieRenderer.material.SetColor("_Color", color);
            }
            else
            {
                if (!(____dieRenderer.sharedMaterial != ____normalMaterial))
                    return false;

                var color = ____dieRenderer.material.GetColor("_Color");
                ____dieRenderer.sharedMaterial = ____normalMaterial;
                ____dieRenderer.material.SetColor("_Color", color);
            }

            return false;
        }
    }
}