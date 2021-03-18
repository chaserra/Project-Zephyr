using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods
{
    public abstract class StatEffect : ScriptableObject, IStatEffect
    {
        public string effectName;

        public abstract void ApplyEffect(); // Initialize and add this effect
        public abstract void Tick(); // Apply effect over time
        public abstract void RemoveEffect(); // Remove / reverse this effect
    }
}