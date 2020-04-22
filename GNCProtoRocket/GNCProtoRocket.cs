using System;
using System.Reflection;
using R2API;
using RoR2;
using UnityEngine;

namespace GNCProtoRocket
{
    class GNCProtoRocket
    {
        internal static ItemIndex GNCProtoRocketItemIndex;
        
        internal static GameObject GNCProtoRocketPrefab;
        private const string ModPrefix = "@GNCProtoRocket:";
        private const string PrefabPath = ModPrefix + "Assets/Import/belt/belt.prefab";
        private const string IconPath = ModPrefix + "Assets/Import/belt_icon/belt_icon.png";

        internal static void Init()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("GNCProtoRocket.exampleitemmod")) 
            {
                Debug.Log(Assembly.GetExecutingAssembly().GetManifestResourceNames()[0]);
                var bundle = AssetBundle.LoadFromStream(stream);
                var provider = new AssetBundleResourcesProvider(ModPrefix.TrimEnd(':'), bundle);
                ResourcesAPI.AddProvider(provider);

                GNCProtoRocketPrefab = bundle.LoadAsset<GameObject>("Assets/Import/belt/belt.prefab");
            }
            
            AddTokens();
            AddGNCProtoRocket();
        }

        private static void AddGNCProtoRocket()
        {
            var GNCProtoRocketItemDef = new ItemDef
            {
                name = "GNCProtoRocket", // its the internal name, no spaces, apostrophes and stuff like that
                tier = ItemTier.Tier1,
                pickupModelPath = PrefabPath,
                pickupIconPath = IconPath,
                nameToken = "GNCProtoRocket_NAME", // stylised name
                pickupToken = "GNCProtoRocket_PICKUP",
                descriptionToken = "GNCProtoRocket_DESC",
                loreToken = "GNCProtoRocket_LORE",
                tags = new[]
                {
                    ItemTag.Utility,
                    ItemTag.Damage
                }
            };

            ItemDisplayRule[] itemDisplayRules = null; // keep this null if you don't want the item to show up on the survivor 3d model. You can also have multiple rules !

            var gncProtoRocket = new R2API.CustomItem(GNCProtoRocketItemDef, itemDisplayRules);

            GNCProtoRocketItemIndex = ItemAPI.Add(gncProtoRocket); // ItemAPI sends back the ItemIndex of your item
        }

        private static void AddTokens()
        {
            //The Name should be self explanatory
            R2API.AssetPlus.Languages.AddToken("GNCProtoRocket_NAME", "GNC Protorocket");
            //The Pickup is the short text that appears when you first pick this up. This text should be short and to the point, nuimbers are generally ommited.
            R2API.AssetPlus.Languages.AddToken("GNCProtoRocket_PICKUP", "Two stones from one bird");
            //The Description is where you put the actual numbers and give an advanced description.
            R2API.AssetPlus.Languages.AddToken("GNCProtoRocket_DESC",
                "Grants <style=cDeath>RAMPAGE</style> on kill. \n<style=cDeath>RAMPAGE</style> : Specifics rewards for reaching kill streaks. \nIncreases <style=cIsUtility>movement speed</style> by <style=cIsUtility>1%</style> <style=cIsDamage>(+1% per item stack)</style> <style=cStack>(+1% every 20 Rampage Stacks)</style>. \nIncreases <style=cIsUtility>damage</style> by <style=cIsUtility>2%</style> <style=cIsDamage>(+2% per item stack)</style> <style=cStack>(+2% every 20 Rampage Stacks)</style>.");
            //The Lore is, well, flavor. You can write pretty much whatever you want here.
            R2API.AssetPlus.Languages.AddToken("GNCProtoRocket_LORE",
                "You were always there, by my side, whether we sat or played. Our friendship was a joyful ride, I wish you could have stayed.");
        }
    }
}
