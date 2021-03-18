using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "NewStatModifier", menuName = "Mods/Stat Effects/Boolean Status")]
    public class StatEffect_BooleanStatus : StatEffect
    {
        public bool flag;
        [Range(0, 1)] public float procChance;

        public override void ApplyEffect()
        {
            // Initialize and add this effect
        }

        public override void Tick()
        {
            // Apply effect over time
        }

        public override void RemoveEffect()
        {
            // Remove / reverse this effect
        }
    }
}