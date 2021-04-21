using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Combat;

namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "NewStatModifier", menuName = "Mods/Stat Effects/Perk")]
    public class StatEffect_Perk : StatEffect
    {
        // Perks are mods that are applied directly to skills.
        // These have the chance to trigger whenever skills are used
        // Perks use the values from an Attack and provide bonuses such as life leech, mana drain, etc.

        //Array of perks here

        // TODO (Perks): Find a way to get Attack

        public override void ApplyEffect(ModifierManager modManager)
        {
            // Iterate through perk array
            // Pass Attack
            // Then trigger their effects with values from said Attack
        }

        public override void Tick(ModifierManager modManager)
        {
            // No need. Perks are instant triggered benefits
        }

        public override void RemoveEffect(ModifierManager modManager)
        {
            // No need. Perks are instant triggered benefits
        }
    }
}