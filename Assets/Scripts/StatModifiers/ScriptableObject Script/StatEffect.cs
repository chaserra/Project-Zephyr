using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods
{
    public abstract class StatEffect : ScriptableObject, IStatEffect
    {
        public string effectName;

        public abstract void ApplyEffect(Modifier mod); // Initialize and add this effect
        public abstract void Tick(Modifier mod); // Apply effect over time
        public abstract void RemoveEffect(Modifier mod); // Remove / reverse this effect
    }
}