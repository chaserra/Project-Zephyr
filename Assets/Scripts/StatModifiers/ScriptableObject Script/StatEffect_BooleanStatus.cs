using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "NewStatModifier", menuName = "Mods/Stat Effects/Boolean Status")]
    public class StatEffect_BooleanStatus : StatEffect
    {
        public bool flag;

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