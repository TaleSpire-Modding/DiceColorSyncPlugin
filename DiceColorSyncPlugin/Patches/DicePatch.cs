using HarmonyLib;
using UnityEngine;

namespace DiceCallbackPlugin.Patches
{
    [HarmonyPatch(typeof(Die), "OnPhotonInstantiate")]
    public class DicePatch
    { 
        static void Postfix(PhotonMessageInfo info, ref Renderer ___dieRenderer)
        {
            if (BoardSessionManager.PhotonIdToClientGuid.TryGetValue(info.sender.ID, out var clientId))
                if (TempClientColorManager.TryGetColor(clientId, out var color))
                    ___dieRenderer.material.SetColor("_Color", Color.red);
        }
    }

    [HarmonyPatch(typeof(Die), "Init")]
    public class DicePatch2
    {
        static void Postfix(ClientGuid clientId, ref Renderer ___dieRenderer)
        {
            if (TempClientColorManager.TryGetColor(clientId, out var color))
                ___dieRenderer.material.SetColor("_Color", color);
        }
    }

    [HarmonyPatch(typeof(Die), "SetMaterial")]
    public class SetMaterialPatch
    {
        static bool Prefix(ref Renderer ___dieRenderer, ref bool gmDie, Material ___normalMaterial, Material ___gmMaterial)
        {
            if (gmDie)
            {
                if (!((UnityEngine.Object)___dieRenderer.sharedMaterial != (UnityEngine.Object)___gmMaterial))
                    return false;

                var color = ___dieRenderer.material.GetColor("_Color");
                ___dieRenderer.sharedMaterial = ___gmMaterial;
                ___dieRenderer.material.SetColor("_Color", color);
            }
            else
            {
                if (!(___dieRenderer.sharedMaterial != ___normalMaterial))
                    return false;

                var color = ___dieRenderer.material.GetColor("_Color");
                ___dieRenderer.sharedMaterial = ___normalMaterial;
                ___dieRenderer.material.SetColor("_Color", color);
            }

            return false;
        }
    }


}