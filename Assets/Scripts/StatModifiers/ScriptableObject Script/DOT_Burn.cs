using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "NewBurn", menuName = "Mods/Stat Effects/Damage Over-Time/Burn")]
    public class DOT_Burn : StatEffect_Ailment
    {
        [Tooltip("Damage dealt per tick.")]
        public int damagePerTick;
        [Tooltip("Add x times damage of DamagePerTick to next tick. 0 if you don't want to add damage per tick.")]
        public float damageMultiplierPerTick = 1f;
        [Tooltip("Max number of times DamageMultiplierPerTick will add to current tick damage.")]
        public int maxTickIncrements = 5;
        [Tooltip("Damage other targets within range.")]
        public float burnRadius = 1.5f;
    }
}