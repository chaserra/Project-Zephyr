using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Stats;

namespace Zephyr.Combat.Mods
{
    [CreateAssetMenu(fileName = "NewDamageOverTime", menuName = "Stat Modifier/Status Effects/Damage Over Time")]
    public class StatEffect_DOT : StatEffect
    {
        public float damagePerTick;

        public override void ApplyStatEffect(CharacterStats stats)
        {
            throw new System.NotImplementedException();
        }
    }
}