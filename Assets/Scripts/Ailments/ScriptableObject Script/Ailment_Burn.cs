using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Combat;
using Zephyr.Util;

namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "Ailment_Burn", menuName = "Mods/Ailment_Ref/Burn")]
    public class Ailment_Burn : Ailment
    {
        private int damagePerTick = 0;
        private int baseDamagePerTick = 0;
        private float damageMultiplier = 0;
        private int maxTickIncrements = 1;
        private int currentTickIncrements = 0;
        private float burnRadius = 0f;
        private DOT_Burn burn;

        public override void InitializeAilment(ModifierManager modifierManager, StatEffect statEffect)
        {
            // Cache modManager
            if (modManager == null)
            {
                modManager = modifierManager;
            }
            // If higher-level ailment is already active, do nothing
            if (!CheckAilmentStatus(statEffect, out burn)) { return; }

            // Set values obtained from SO
            tickInterval = burn.tickInterval;
            baseDamagePerTick = burn.damagePerTick;
            damageMultiplier = burn.damageMultiplierPerTick;
            maxTickIncrements = burn.maxTickIncrements;
            currentAilmentLevel = burn.ailmentLevel;
            burnRadius = burn.burnRadius;
            isActive = true;

            // Prevent current burn damage from being overwritten if current damage is higher
            if (damagePerTick > burn.damagePerTick) { return; }
            damagePerTick = burn.damagePerTick;
        }

        public override void RemoveAilment(ModifierManager modifierManager, StatEffect statEffect)
        {
            // Reset values
            // If higher-level ailment is already active, do nothing
            if (!CheckAilmentStatus(statEffect, out burn)) { return; }
            ResetBaseAilmentValues();
            damagePerTick = 0;
            baseDamagePerTick = 0;
            damageMultiplier = 0;
            maxTickIncrements = 1;
            currentTickIncrements = 0;
            burnRadius = 0f;
        }

        public override void Tick(ModifierManager modifierManager)
        {
            if (isActive)
            {
                if (tickTimer <= 0)
                {
                    // Create attack
                    var attack = new Attack(damagePerTick, textColor);
                    modManager.DealDamage(attack);

                    // Get other targets
                    Collider[] col = Physics.OverlapSphere(modifierManager.gameObject.transform.position, burnRadius);

                    // Deal damage to those within range
                    for (int i = col.Length - 1; i >= 0; i--)
                    {
                        if (col[i].gameObject == modifierManager.gameObject) { continue; } // Ignore self
                        if (col[i].gameObject.tag != modifierManager.gameObject.tag) { continue; } // Only affect same tag (Player or Enemy)
                        
                        ModifierManager target = col[i].GetComponent<ModifierManager>();
                        if (target != null)
                        {
                            if (target.AilmentActive(this)) { continue; } // Ignore already burning enemy
                            int damageWithFalloff = UtilityHelper.DamageDistanceFallOff(modifierManager.gameObject.transform.position, target.transform.position, burnRadius, damagePerTick);
                            if (damageWithFalloff == 0) { continue; } // Don't display 0 damage
                            var areaBurn = new Attack(damageWithFalloff, textColor);
                            target.DealDamage(areaBurn);
                        }
                    }

                    // Increment next burn damage tick
                    if (currentTickIncrements < maxTickIncrements)
                    {
                        float newDamage = baseDamagePerTick * damageMultiplier;
                        damagePerTick += Mathf.RoundToInt(newDamage);
                        currentTickIncrements++;
                    }

                    // Reset tick timer
                    tickTimer = tickInterval;
                }
                tickTimer -= Time.deltaTime;
            }
        }

    }
}