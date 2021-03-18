﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "NewStatModifier", menuName = "Mods/Stat Effects/Modify Stat Values")]
    public class StatEffect_ModifyStats : StatEffect
    {
        public StatList targetStat;
        public bool isPercentage;
        public float modifierValue;

        public override void ApplyEffect()
        {
            // Initialize and add this effect
        }

        public override void Tick()
        {
            // Apply effect over time
        }

        public override void RemoveEffect()
        {
            // Remove / reverse this effect
        }
    }
}