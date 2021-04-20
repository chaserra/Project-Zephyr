using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "NewBurn", menuName = "Mods/Stat Effects/Damage Over-Time/Burn")]
    public class DOT_Burn : StatEffect_Ailment
    {
        public int damagePerTick;
        public float damageMultiplierPerTick = 1f;
    }
}