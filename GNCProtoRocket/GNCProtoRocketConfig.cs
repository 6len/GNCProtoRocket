using BepInEx.Configuration;

namespace GNCProtoRocket
{
    class GNCProtoRocketConfig
    {
        internal static ConfigEntry<float> GNCProtoRocketNumber;
        internal static ConfigEntry<float> GNCProtoRocketBaseDamage;
        internal static ConfigEntry<float> GNCProtoRocketProcChance;

        internal static void Load()
        {
            GNCProtoRocketNumber = GncProtoRocketPlugin.Instance.Config.Bind<float>("GNCProtoRocket Settings", "Rocket Amount",
                1f, "Controls the amount of rockets initially fired."); 
            GNCProtoRocketBaseDamage = GncProtoRocketPlugin.Instance.Config.Bind<float>("GNCProtoRocket Settings", "Base Damage",
                20f, "Controls the base damage of each rocket"); 
            GNCProtoRocketProcChance = GncProtoRocketPlugin.Instance.Config.Bind<float>("GNCProtoRocket Settings", "Proc Chance",
                12.5f, "Controls the proc chance of rockets firing.");
        }
    }
}