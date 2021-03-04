using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Stats;

namespace Zephyr.Combat.Mods
{
    [CreateAssetMenu(fileName = "NewStatModifier", menuName = "Stat Modifier/Status Effects/Stat Modifier")]
    public class StatEffect_StatMultiplier : StatEffect
    {
        public float multiplier;

        public override void ApplyStatEffect(CharacterStats stats)
        {
            throw new System.NotImplementedException();
        }
    }
}