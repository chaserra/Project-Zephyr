using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "NewStatModifier", menuName = "Mods/Stat Effects/Boolean Status")]
    public class StatEffect_BooleanStatus : StatEffect
    {
        public bool flag;

        public override void ApplyEffect(Modifier mod)
        {
            // Initialize and add this effect
        }

        public override void Tick(Modifier mod)
        {
            // Apply effect over time
        }

        public override void RemoveEffect(Modifier mod)
        {
            // Remove / reverse this effect
        }
    }
}