using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "NewPoison", menuName = "Mods/Stat Effects/Damage Over-Time/Poison")]
    public class DOT_Poison : StatEffect_Ailment
    {
        [Tooltip("Percent of max health damage dealt per tick.")]
        public float percentDamagePerTick;
    }
}