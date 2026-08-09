using System.Reflection;
using HarmonyLib;
using SouvyZiplineCustomizer.Settings;
using Timberborn.CharacterMovementSystem;
using Timberborn.ZiplineSystem;

namespace SouvyZiplineCustomizer.Patches
{
    [HarmonyPatch(typeof(ZiplineConnectionService), "InclinationIsValid")]
    public static class InclinationIsValidPatch
    {
        public static void Postfix(ref float inclination, ref float maxInclination, ref bool __result)
        {
            if (ZiplineCustomizerSettingsOwner.Instance != null)
            {
                maxInclination = ZiplineCustomizerSettingsOwner.Instance.MaxAngleSetting.Value;
                __result = inclination <= maxInclination;
            }
        }
    }

    [HarmonyPatch(typeof(ZiplineConnectionService), "DistanceIsValid")]
    public static class DistanceIsValidPatch
    {
        public static void Postfix(ref float distance, ref float maxDistance, ref bool __result)
        {
            if (ZiplineCustomizerSettingsOwner.Instance != null)
            {
                maxDistance = ZiplineCustomizerSettingsOwner.Instance.MaxDistanceSetting.Value;
                __result = distance <= maxDistance;
            }
        }
    }

    [HarmonyPatch(typeof(ZiplineConnectionService), "Load")]
    public static class ZiplineConnectionServiceLoadPatch
    {
        public static void Postfix(ZiplineConnectionService __instance)
        {
            if (ZiplineCustomizerSettingsOwner.Instance != null)
            {
                var field = typeof(ZiplineConnectionService).GetField("_maxCableInclination", BindingFlags.NonPublic | BindingFlags.Instance);
                if (field != null)
                {
                    field.SetValue(__instance, ZiplineCustomizerSettingsOwner.Instance.MaxAngleSetting.Value);
                }
            }
        }
    }

    [HarmonyPatch(typeof(ZiplineTower), "get_MaxConnections")]
    public static class ZiplineTowerMaxConnectionsPatch
    {
        public static void Postfix(ref int __result)
        {
            if (ZiplineCustomizerSettingsOwner.Instance != null)
            {
                __result = ZiplineCustomizerSettingsOwner.Instance.MaxConnectionsSetting.Value;
            }
        }
    }

    [HarmonyPatch]
    public static class ZiplineTowerSpecMaxConnectionsPatch
    {
        public static MethodBase TargetMethod()
        {
            var type = AccessTools.TypeByName("Timberborn.ZiplineSystem.ZiplineTowerSpec");
            return AccessTools.PropertyGetter(type, "MaxConnections");
        }

        public static void Postfix(ref int __result)
        {
            if (ZiplineCustomizerSettingsOwner.Instance != null)
            {
                __result = ZiplineCustomizerSettingsOwner.Instance.MaxConnectionsSetting.Value;
            }
        }
    }

    [HarmonyPatch(typeof(ZiplineTower), "get_HasFreeSlots")]
    public static class ZiplineTowerHasFreeSlotsPatch
    {
        public static void Postfix(ZiplineTower __instance, ref bool __result)
        {
            if (ZiplineCustomizerSettingsOwner.Instance != null)
            {
                __result = __instance.ConnectionTargets.Count < ZiplineCustomizerSettingsOwner.Instance.MaxConnectionsSetting.Value;
            }
        }
    }

    [HarmonyPatch(typeof(ZiplineTower), "get_MaxDistance")]
    public static class ZiplineTowerMaxDistancePatch
    {
        public static void Postfix(ref int __result)
        {
            if (ZiplineCustomizerSettingsOwner.Instance != null)
            {
                __result = ZiplineCustomizerSettingsOwner.Instance.MaxDistanceSetting.Value;
            }
        }
    }

    [HarmonyPatch]
    public static class ZiplineTowerSpecMaxDistancePatch
    {
        public static MethodBase TargetMethod()
        {
            var type = AccessTools.TypeByName("Timberborn.ZiplineSystem.ZiplineTowerSpec");
            return AccessTools.PropertyGetter(type, "MaxDistance");
        }

        public static void Postfix(ref int __result)
        {
            if (ZiplineCustomizerSettingsOwner.Instance != null)
            {
                __result = ZiplineCustomizerSettingsOwner.Instance.MaxDistanceSetting.Value;
            }
        }
    }

    [HarmonyPatch(typeof(MovementSpeedBoostingBuildingSpec), "get_BoostPercentage")]
    public static class MovementSpeedBoostingBuildingSpecPatch
    {
        public static void Postfix(ref int __result)
        {
            if (ZiplineCustomizerSettingsOwner.Instance != null)
            {
                // SpeedMultiplierSetting is 1x to 5x (100% to 500% speed)
                __result = ZiplineCustomizerSettingsOwner.Instance.SpeedMultiplierSetting.Value * 100;
            }
        }
    }
}
