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
        private float damageMultiplier = 0; // TODO (Burn): Find way to make this editable in inspector

        public override void InitializeAilment(ModifierManager modifierManager, StatEffect statEffect)
        {
            // Cache modManager
            if (modManager == null)
            {
                modManager = modifierManager;
            }
            // Cast Stat Effect as DOT
            var burn = statEffect as DOT_Burn;
            if (burn == null) { return; }
            // Set values obtained from SO
            tickInterval = burn.tickInterval;
            damagePerTick = burn.damagePerTick;
            baseDamagePerTick = burn.damagePerTick;
            damageMultiplier = burn.damageMultiplierPerTick;
            isActive = true;
        }

        public override void RemoveAilment(ModifierManager modifierManager)
        {
            // Reset values
            isActive = false;
            tickInterval = 0;
            tickTimer = 0;
            damagePerTick = 0;
            baseDamagePerTick = 0;
            damageMultiplier = 0;
        }

        public override void Tick(ModifierManager modifierManager)
        {
            if (isActive)
            {
                // TODO (Burn): Add code that will affect other nearby enemies (rng chance)
                if (tickTimer <= 0)
                {
                    // Create attack
                    var attack = new Attack(damagePerTick, damageTextColor);

                    modManager.DealDamage(attack);

                    // Increment next burn damage tick
                    float newDamage = baseDamagePerTick * damageMultiplier;
                    damagePerTick += Mathf.RoundToInt(newDamage);

                    tickTimer = tickInterval;
                }
                tickTimer -= Time.deltaTime;
            }
        }
    }
}