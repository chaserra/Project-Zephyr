using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Stats;

namespace Zephyr.Mods
{
    [RequireComponent(typeof(CharacterStats))]
    public class ModifierManager : MonoBehaviour
    {
        // Cache
        private CharacterStats characterStats;

        // State
        [SerializeField] private List<Modifier> modifiers = new List<Modifier>();

        private void Awake()
        {
            characterStats = GetComponent<CharacterStats>();
        }

        public void AddModifier(Modifier mod)
        {
            modifiers.Add(mod);
            mod.InitializeModifier(this);
        }

        public void RemoveModifier(Modifier mod)
        {
            mod.RemoveStatEffects();
            modifiers.Remove(mod);
        }

        // TODO HIGH-PRIO (Mods): Put all aggregate logic here in mod manager
        // Do summation of total flat and percentage here and just pass values to charStats
        public void AggregateStatValues(StatList targetStat, float value, bool isPercentage, bool reverseValues)
        {
            characterStats.ModifyStat(targetStat, value, isPercentage, reverseValues);
        }

        private void OnDisable()
        {
            // Failsafe
            StopAllCoroutines();
            modifiers.Clear();
        }

    }
}