using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "NewBurn", menuName = "Mods/Stat Effects/Recovery Over-Time/Blessing")]
    public class HOT_Heal : StatEffect_Ailment
    {
        public float healPerTick;
        public float healMultiplierPerTick = 0f;
        public bool isPercentage = false;
    }
}