using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using R2API;
using R2API.AssetPlus;
using R2API.Utils;
using UnityEngine;

namespace GNCProtoRocket
{
    [BepInDependency(R2API.R2API.PluginGUID)]
    [R2APISubmoduleDependency(nameof(AssetPlus), nameof(ItemAPI), nameof(ItemDropAPI), nameof(ResourcesAPI))]
    [BepInPlugin(ModGuid, ModName, ModVer)]
    public class GncProtoRocketPlugin : BaseUnityPlugin
    {   
        private const string ModVer = "1.0.0";
        private const string ModName = "GNCProtoRocket";
        public const string ModGuid = "com.GlenCloughley.GNCProtoRocket";
        internal static GncProtoRocketPlugin Instance;

        public void Awake()
        {
            if (Instance == null) { Instance = this; }
            GNCProtoRocketConfig.Load();
            GNCProtoRocket.Init();
            Hooks.Init();
        }
    }
}