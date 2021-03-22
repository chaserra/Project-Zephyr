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
        [SerializeField] private List<Modifier> modifiers = new List<Modifier>(); // TODO (cleanup): Remove serializefield

        private void Awake()
        {
            characterStats = GetComponent<CharacterStats>();
        }

        #region MODIFIER MANAGEMENT
        public void AddModifier(Modifier mod_template)
        {
            // TODO (Modifier Management): Create a new list of mod references.
            // Check if mod reference exists in new list. If not, add reference.
            // Use reference to track duration, etc.
            Modifier mod = Instantiate(mod_template); // Prevent from saving over scriptable object file
            modifiers.Add(mod);
            mod.InitializeModifier(this);
        }

        public void RemoveModifier(Modifier mod)
        {
            mod.RemoveStatEffects();
        }

        public void RemoveModifierFromList(Modifier mod)
        {
            modifiers.Remove(mod);
        }
        #endregion

        #region STAT MODIFIERS (Buffs, Debuffs)
        public void AggregateStatValues(StatList targetStat, float value, bool isPercentage, bool reverseValues)
        {
            // Reverse values for removing stat mods
            if (reverseValues)
            {
                value *= -1;
            }

            // Compute Mod Sheet values (total per stat and type of modification)
            characterStats.AggregateStatSheetValues(targetStat, value, isPercentage);
        }
        #endregion

        private void OnDisable()
        {
            // Failsafe
            StopAllCoroutines();
            modifiers.Clear();
        }

    }
}