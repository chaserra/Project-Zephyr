using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.UI;

namespace Zephyr.Mods
{
    public abstract class StatEffect : ScriptableObject, IStatEffect
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperNotes;
        [Space]
#endif
        public string effectName;
        public UIStatEffect_SO uiImage;

        public abstract void ApplyEffect(ModifierManager modManager); // Initialize and add this effect
        public abstract void Tick(ModifierManager modManager); // Apply effect over time
        public abstract void RemoveEffect(ModifierManager modManager); // Remove / reverse this effect
    }
}