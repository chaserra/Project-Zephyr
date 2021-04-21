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

        public override void InitializeAilment(ModifierManager modifierManager, StatEffect statEffect)
        {
            // Cache modManager
            if (modManager == null)
            {
                modManager = modifierManager;
            }
            // Cast Stat Effect as DOT
            var blessing = statEffect as HOT_Heal;
            if (blessing == null) { return; }

            // Set values obtained from SO
            tickInterval = blessing.tickInterval;
            healPerTick = blessing.healPerTick;
            baseHealPerTick = blessing.healPerTick;
            healMultiplierPerTick = blessing.healMultiplierPerTick;
            isPercentage = blessing.isPercentage;
            isActive = true;
        }

        public override void RemoveAilment(ModifierManager modifierManager)
        {
            // Reset Values
            isActive = false;
            tickTimer = 0;
            tickInterval = 0;
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
                    var attack = new Attack(convertedHealValue, textColor);
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