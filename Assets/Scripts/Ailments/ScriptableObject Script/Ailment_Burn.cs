using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Combat;

namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "Ailment_Burn", menuName = "Mods/Ailment_Ref/Burn")]
    public class Ailment_Burn : Ailment
    {
        // TODO (Burn): Recode this as below code are all placeholder for testing
        // IDEAS: all nearby enemies get burned as well. But burn should not reset every time it
            // procs to avoid infinite burn loop
        private int damagePerTick = 0;
        private int baseDamagePerTick = 0;
        private float damageMultiplier = 0;
        private int maxTickIncrements = 1;
        private int currentTickIncrements = 0;
        private DOT_Burn burn;

        public override void InitializeAilment(ModifierManager modifierManager, StatEffect statEffect)
        {
            // Cache modManager
            if (modManager == null)
            {
                modManager = modifierManager;
            }
            // Check if ailment is already active
            if (!CheckAilmentStatus(statEffect, out burn)) { return; }

            // Set values obtained from SO
            tickInterval = burn.tickInterval;
            baseDamagePerTick = burn.damagePerTick;
            damageMultiplier = burn.damageMultiplierPerTick;
            maxTickIncrements = burn.maxTickIncrements;
            currentAilmentLevel = burn.ailmentLevel;
            isActive = true;

            // Prevent current burn damage from being overwritten if current damage is higher
            if (damagePerTick > burn.damagePerTick) { return; }
            damagePerTick = burn.damagePerTick;
        }

        public override void RemoveAilment(ModifierManager modifierManager, StatEffect statEffect)
        {
            // Reset values
            if (!CheckAilmentStatus(statEffect, out burn)) { return; }
            ResetBaseAilmentValues();
            damagePerTick = 0;
            baseDamagePerTick = 0;
            damageMultiplier = 0;
            maxTickIncrements = 1;
            currentTickIncrements = 0;
        }

        public override void Tick(ModifierManager modifierManager)
        {
            if (isActive)
            {
                // TODO (Burn): Add code that will affect other nearby enemies (rng chance)
                if (tickTimer <= 0)
                {
                    // Create attack
                    var attack = new Attack(damagePerTick, textColor);
                    modManager.DealDamage(attack);

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