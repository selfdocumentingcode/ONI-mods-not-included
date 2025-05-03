// Decompiled with JetBrains decompiler
// Type: NamedStorage.NamedStoragePatches
// Assembly: NamedStorage, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 4FBF6D13-39C0-4212-95AB-5935240514FB
// Assembly location: %USERPROFILE%\Documents\Klei\OxygenNotIncluded\mods\Steam\2991018571\NamedStorage.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;
using HarmonyLib;
using KMod;
using UnityEngine;

namespace NamedBigStorage
{
    public class NamedBigStoragePatches : UserMod2
    {
        private static readonly Dictionary<string, string> BigStorageConfigs = new Dictionary<string, string>
        {
            { "BigBeautifulStorageLockerConfig", "ConfigureBuildingTemplate" },
            { "BigRefrigeratorConfig", "ConfigureBuildingTemplate" },
            { "BigSmartStorageLockerConfig", "DoPostConfigureComplete" },
            { "BigStorageLockerConfig", "ConfigureBuildingTemplate" },
            { "BigStorageTileConfig", "ConfigureBuildingTemplate" },
        };

        public override void OnLoad(Harmony harmony)
        {
            HarmonyLib.FileLog.Log($"OnLoad {nameof(NamedBigStoragePatches)}: Start");

            foreach (var (typeName, originalMethodName) in BigStorageConfigs)
            {
                var typeByName = AccessTools.TypeByName(typeName);

                if (typeByName == null)
                {
                    HarmonyLib.FileLog.Log($"typeByName is null for {typeName}");
                    continue;
                }

                HarmonyLib.FileLog.Log("typeByName: " + typeByName);

                var original = typeByName.GetMethod(originalMethodName);
                HarmonyLib.FileLog.Log("original: " + original);

                var postfix = typeof(NamedBigStoragePatches).GetMethod(nameof(AddComponent));
                HarmonyLib.FileLog.Log("postfix: " + postfix);

                harmony.Patch(original, postfix: new HarmonyMethod(postfix));
            }

            base.OnLoad(harmony);

            HarmonyLib.FileLog.Log($"OnLoad {nameof(NamedBigStoragePatches)}: Done");
        }

        [HarmonyPatch(typeof(CreatureFeederConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class CreatureFeederConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go) => NamedBigStoragePatches.AddComponent(go);
        }

        [HarmonyPatch(typeof(FishFeederConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class FishFeederConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go) => NamedBigStoragePatches.AddComponent(go);
        }

        [HarmonyPatch(typeof(RationBoxConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class RationBoxConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go) => NamedBigStoragePatches.AddComponent(go);
        }

        [HarmonyPatch(typeof(RefrigeratorConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class RefrigeratorConfig_DoPostConfigureComplete_Patch
        {
            public static void Postfix(GameObject go) => NamedBigStoragePatches.AddComponent(go);
        }

        [HarmonyPatch(typeof(StorageLockerConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class StorageLockerConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go) => NamedBigStoragePatches.AddComponent(go);
        }

        [HarmonyPatch(typeof(StorageLockerSmartConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class StorageLockerSmartConfig_DoPostConfigureComplete_Patch
        {
            public static void Postfix(GameObject go) => NamedBigStoragePatches.AddComponent(go);
        }

        [HarmonyPatch(typeof(ObjectDispenserConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class ObjectDispenserConfig_DoPostConfigureComplete_Patch
        {
            public static void Postfix(GameObject go) => NamedBigStoragePatches.AddComponent(go);
        }

        [HarmonyPatch(typeof(SolidConduitInboxConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class SolidConduitInboxConfig_DoPostConfigureComplete_Patch
        {
            public static void Postfix(GameObject go) => NamedBigStoragePatches.AddComponent(go);
        }

        [HarmonyPatch(typeof(BottleEmptierConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class BottleEmptierConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go) => NamedBigStoragePatches.AddComponent(go);
        }

        [HarmonyPatch(typeof(BottleEmptierGasConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class BottleEmptierGasConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go) => NamedBigStoragePatches.AddComponent(go);
        }

        [HarmonyPatch(typeof(TreeFilterable))]
        [HarmonyPatch("UpdateFilters")]
        public class TreeFilterable_UpdateFilters_Patch
        {
            public static void Postfix(Storage ___storage, HashSet<Tag> filters)
            {
                if (filters == null)
                    return;
                NamedStorageImplement component = ___storage.GetComponent<NamedStorageImplement>();
                if (!(component != null) || !component.SetReplaceNameStorage())
                    return;
                component.Trigger(1980521255, ___storage.gameObject);
            }
        }

        public static void AddComponent(GameObject go) => go.AddOrGet<NamedStorageImplement>();
    }
}