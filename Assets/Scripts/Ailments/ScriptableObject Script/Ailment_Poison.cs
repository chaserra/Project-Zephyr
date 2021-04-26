using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Combat;

namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "Ailment_Poison", menuName = "Mods/Ailment_Ref/Poison")]
    public class Ailment_Poison : Ailment
    {
        private float percentDamagePerTick = 0;
        private DOT_Poison poison;

        public override void InitializeAilment(ModifierManager modifierManager, StatEffect statEffect)
        {
            // Cache modManager
            if (modManager == null)
            {
                modManager = modifierManager;
            }
            // Check if ailment is already active
            if (!CheckAilmentStatus(statEffect, out poison)) { return; }

            // Set values obtained from SO
            tickInterval = poison.tickInterval;
            percentDamagePerTick = poison.percentDamagePerTick;
            currentAilmentLevel = poison.ailmentLevel;
            isActive = true;
        }

        public override void RemoveAilment(ModifierManager modifierManager, StatEffect statEffect)
        {
            // Reset values
            if (!CheckAilmentStatus(statEffect, out poison)) { return; }
            ResetBaseAilmentValues();
            percentDamagePerTick = 0;
        }

        public override void Tick(ModifierManager modifierManager)
        {
            if (isActive)
            {
                if (tickTimer <= 0)
                {
                    // Get computed damage from health percentage
                    var computedDamage = modManager.GetHealthPercentage(percentDamagePerTick);

                    // Create attack
                    var attack = new Attack(computedDamage, textColor);
                    modManager.DealDamage(attack);

                    // Reset tick timer
                    tickTimer = tickInterval;
                }
                tickTimer -= Time.deltaTime;
            }
        }
    }
}