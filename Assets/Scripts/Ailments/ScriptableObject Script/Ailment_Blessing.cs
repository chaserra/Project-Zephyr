using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Combat;

namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "Ailment_Poison", menuName = "Mods/Ailment_Ref/Blessing")]
    public class Ailment_Blessing : Ailment
    {
        private float healPerTick;
        private float baseHealPerTick;
        private float healMultiplierPerTick = 0f;
        private bool isPercentage = false;
        private HOT_Heal blessing;

        public override void InitializeAilment(ModifierManager modifierManager, StatEffect statEffect)
        {
            // Cache modManager
            if (modManager == null)
            {
                modManager = modifierManager;
            }
            // Check if ailment is already active
            if (!CheckAilmentStatus(statEffect, out blessing)) { return; }

            // Set values obtained from SO
            tickInterval = blessing.tickInterval;
            baseHealPerTick = blessing.healPerTick;
            healMultiplierPerTick = blessing.healMultiplierPerTick;
            isPercentage = blessing.isPercentage;
            isActive = true;

            // Prevent current heal from being overwritten if current heal is higher
            if (healPerTick > blessing.healPerTick) { return; }
            healPerTick = blessing.healPerTick;
        }

        public override void RemoveAilment(ModifierManager modifierManager, StatEffect statEffect)
        {
            // Reset Values
            if (!CheckAilmentStatus(statEffect, out blessing)) { return; }
            ResetBaseAilmentValues();
            healPerTick = 0f;
            baseHealPerTick = 0f;
            healMultiplierPerTick = 0f;
            isPercentage = false;
        }

        public override void Tick(ModifierManager modifierManager)
        {
            if (isActive)
            {
                if (tickTimer <= 0)
                {
                    int convertedHealValue;

                    // Percent heal
                    if (isPercentage)
                    {
                        // Get computed heal from health percentage
                        convertedHealValue = modManager.GetHealthPercentage(healPerTick);
                    }
                    // Flat heal
                    else
                    {
                        // Convert float value to int
                        convertedHealValue = Mathf.RoundToInt(healPerTick);
                    }
                    // Create attack
                    var attack = new Attack(convertedHealValue);
                    modManager.DealHealing(attack);

                    // Increment next heal recovery tick
                    float newHeal = baseHealPerTick * healMultiplierPerTick;
                    healPerTick += Mathf.RoundToInt(newHeal);

                    // Reset tick timer
                    tickTimer = tickInterval;
                }
                tickTimer -= Time.deltaTime;
            }
        }
    }
}