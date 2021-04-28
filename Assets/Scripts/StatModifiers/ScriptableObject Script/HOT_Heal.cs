using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "NewBurn", menuName = "Mods/Stat Effects/Recovery Over-Time/Blessing")]
    public class HOT_Heal : StatEffect_Ailment
    {
        [Tooltip("Heal received per tick.")]
        public float healPerTick;
        [Tooltip("Add x times recovery of HealPerTick to next tick. 0 if you don't want to add healing per tick.")]
        public float healMultiplierPerTick = 0f;
        [Tooltip("True = Healing dealt will be calculated as percentage of max health.")]
        public bool isPercentage = false;
        [Tooltip("Max number of times DamageMultiplierPerTick will add to current tick damage.")]
        public int maxTickIncrements = 5;
    }
}