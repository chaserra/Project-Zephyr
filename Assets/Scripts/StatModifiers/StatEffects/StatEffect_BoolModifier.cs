using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Stats;

namespace Zephyr.Combat.Mods
{
    [CreateAssetMenu(fileName = "NewStatModifier", menuName = "Stat Modifier/Status Effects/Boolean Modifier")]
    public class StatEffect_BoolModifier : StatEffect
    {
        public bool flag;

        public override void ApplyStatEffect(CharacterStats stats)
        {
            throw new System.NotImplementedException();
        }
    }
}