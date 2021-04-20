using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "NewStatModifier", menuName = "Mods/Stat Effects/Perk")]
    public class StatEffect_Perk : StatEffect
    {
        

        public override void ApplyEffect(ModifierManager modManager)
        {
            // Do perk stuff
        }

        public override void Tick(ModifierManager modManager)
        {
            // Apply effect over time
        }

        public override void RemoveEffect(ModifierManager modManager)
        {
            // Remove perk stuff
        }
    }
}