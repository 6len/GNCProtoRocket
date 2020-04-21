using System.Runtime.CompilerServices;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace GNCProtoRocket
{
    public class Hooks
    {
        internal static void Init()
        {
            On.RoR2.GlobalEventManager.OnCharacterDeath += GNCProtoRocket_OnKill;
        }


        private static void GNCProtoRocket_OnKill(On.RoR2.GlobalEventManager.orig_OnCharacterDeath orig, GlobalEventManager gem,
            DamageReport damageReport)
        {
            try
            {
                if (damageReport.damageInfo.attacker)
                {
                    Inventory Inv = damageReport.damageInfo.attacker.GetComponent<CharacterBody>().inventory;
                    int GNCProtoRocketCount = Inv.GetItemCount(GNCProtoRocket.GNCProtoRocketItemIndex);
                    if (GNCProtoRocketCount > 0)
                    {
                        CharacterBody component = damageReport.damageInfo.attacker.GetComponent<CharacterBody>();
                        TeamComponent component2 = component.GetComponent<TeamComponent>();
                        TeamIndex teamIndex = component2 ? component2.teamIndex : TeamIndex.Neutral;
                        
                        if (component)
                        {
                            CharacterMaster master = component.master;
                            if (master)
                            {
                               ProcMissile(GNCProtoRocketCount, component, master, teamIndex, damageReport.damageInfo.procChainMask, null, damageReport.damageInfo);
                            }
                        }
                    }
                }
            }
            catch
            {
            }

            orig(gem, damageReport);
        }

        private static void ProcMissile(int stack, CharacterBody attackerBody, CharacterMaster attackerMaster,
            TeamIndex attackerTeamIndex, ProcChainMask procChainMask, GameObject victim, DamageInfo damageInfo)
        {
            if (stack > 0)
            {
                GameObject gameObject = attackerBody.gameObject;
                InputBankTest component = gameObject.GetComponent<InputBankTest>();
                Vector3 position = component ? component.aimOrigin : gameObject.transform.position;
                Vector3 vector = component ? component.aimDirection : gameObject.transform.forward;
                Vector3 up = Vector3.up;

                if (Util.CheckRoll(10f * GNCProtoRocketConfig.GNCProtoRocketProcChance.Value, attackerMaster))
                {
                    float damageCoefficient = 3f * (float) stack;
                    float damage = Util.OnHitProcDamage(damageInfo.damage, attackerBody.damage, damageCoefficient);
                    ProcChainMask procChainMask2 = procChainMask;
                    procChainMask2.AddProc(ProcType.Missile);
                    FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
                    {
                        projectilePrefab = GlobalEventManager.instance.missilePrefab,
                        position = position,
                        rotation = Util.QuaternionSafeLookRotation(up),
                        procChainMask = procChainMask2,
                        target = victim,
                        owner = gameObject,
                        damage = damage,
                        crit = damageInfo.crit,
                        force = 200f,
                        damageColorIndex = DamageColorIndex.Item
                    };
                    ProjectileManager.instance.FireProjectile(fireProjectileInfo);
                }
            }
        }
    }
}
