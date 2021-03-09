using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "NewStatModifier", menuName = "Mods/Stat Effects/Modify Stat(Percentage)")]
    public class StatEffect_ModifyStats : StatEffect
    {
        public bool isPercentage;
        public float modifierValue;

        public override void ApplyEffect(ModifierManager modManager)
        {
            // Initialize and add this effect
        }

        public override void Tick(ModifierManager modManager)
        {
            // Apply effect over time
        }

        public override void RemoveEffect(ModifierManager modManager)
        {
            // Remove / reverse this effect
        }
    }
}