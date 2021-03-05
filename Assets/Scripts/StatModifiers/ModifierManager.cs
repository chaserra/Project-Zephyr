using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Stats;

namespace Zephyr.Combat.Mods
{
    [RequireComponent(typeof(CharacterStats))]
    public class ModifierManager : MonoBehaviour
    {
        private CharacterStats stats;
        private List<Modifier> activeModifiers = new List<Modifier>();

        private void Awake()
        {
            stats = GetComponent<CharacterStats>();
        }

        private void Update()
        {
            ApplyModifiers();
        }

        private void ApplyModifiers()
        {
            if (activeModifiers.Count < 1) { return; }
            foreach (Modifier mod in activeModifiers)
            {
                mod.ApplyModifier(stats);
            }
        }

        public void AddModifier(Modifier modToAdd)
        {
            activeModifiers.Add(modToAdd);
        }

        public void DisableModifier(Modifier modToDisable)
        {
            foreach (Modifier mod in activeModifiers)
            {
                if (mod.Equals(modToDisable))
                {
                    activeModifiers.Remove(mod);
                }
            }
        }

    }
}