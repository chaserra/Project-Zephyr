﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods
{
    public abstract class StatEffect : ScriptableObject, IStatEffect
    {
        public StatList targetStat;

        public abstract void ApplyEffect(ModifierManager modManager); // Initialize and add this effect
        public abstract void Tick(ModifierManager modManager); // Apply effect over time
        public abstract void RemoveEffect(ModifierManager modManager); // Remove / reverse this effect
    }
}