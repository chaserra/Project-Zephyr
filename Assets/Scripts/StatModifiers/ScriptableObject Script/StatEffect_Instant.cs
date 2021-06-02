using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Instant StatEffect applies an immediate effect to the mod's target
 * Does not Tick
 */
namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "NewInstant", menuName = "Mods/Stat Effects/Instant Container")]
    public class StatEffect_Instant : StatEffect
    {
        public Instant[] instants;

        public override void ApplyEffect(ModifierManager modManager)
        {
            for (int i = 0; i < instants.Length; i++)
            {
                instants[i].CastInstant(modManager);
            }
        }

        public override void RemoveEffect(ModifierManager modManager)
        {
            // Not needed as effect is instant
        }

        public override void Tick(ModifierManager modManager)
        {
            // Not needed as effect is instant
        }
    }
}