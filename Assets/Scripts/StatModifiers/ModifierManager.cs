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
        [SerializeField] private AilmentsList ailmentsList_Template;
        [SerializeField] private AilmentsList ailmentsList; // TODO (cleanup): remove SerializeField

        // State
        private List<ModifierWrapper> modWrappers = new List<ModifierWrapper>();
        
        // Properties
        public AilmentsList AilmentsList { get { return ailmentsList; } }

        private void Awake()
        {
            characterStats = GetComponent<CharacterStats>();
            if (ailmentsList_Template == null) { Debug.LogWarning("Missing Ailments List for " + gameObject.name); return; }
            ailmentsList = Instantiate(ailmentsList_Template);
            ailmentsList.Initialize(this);
        }

        private void Update()
        {
            if (modWrappers.Count > 0)
            {
                foreach (ModifierWrapper wrapper in modWrappers)
                {
                    wrapper.Mod.Tick(this);
                }
            }

            if (ailmentsList != null)
            {
                ailmentsList.Tick();
            }
        }

        #region MODIFIER MANAGEMENT
        public void AddModifier(Modifier modifier)
        {
            // Roll for proc
            if (!modifier.ProcModifier()) { return; }

            ModifierWrapper existingWrapper = ExistingMod(modifier);

            if (existingWrapper != null) // If mod exists
            {
                // Check if stackable
                if (existingWrapper.Mod.Context.isStackable) 
                {
                    // Stack buff to the list
                    existingWrapper.ReapplyModifiers();
                } 
                // Reset duration
                existingWrapper.ResetModDuration();
            }
            else // If mod is new
            {
                ModifierWrapper newWrapper = WrapModifier(modifier);
                modWrappers.Add(newWrapper);
                newWrapper.InitializeWrapper();
            }
            Debug.Log("Stat effect " + modifier.name + " added to " + gameObject.name);
        }

        /**
         * Removes Buffs or Debuffs (Type-specific)
         **/
        public void RemoveModType(ModType type)
        {
            // Iterate through list of wrappers
            for (int i = modWrappers.Count - 1; i >= 0; i--)
            {
                // Compare mod type
                if (modWrappers[i].Mod.Context.modType == type)
                {
                    RemoveModifier(modWrappers[i].Mod);
                }
            }
        }

        /** 
         * Remove target modifier 
         **/
        public void RemoveModifier(Modifier modifier)
        {
            ModifierWrapper existingWrapper = ExistingMod(modifier);
            if (existingWrapper == null) { return; }
            existingWrapper.DeactivateMod();
            for (int i = 0; i < existingWrapper.CurrentStacks; i++)
            {
                // Remove effect stacks
                existingWrapper.Mod.RemoveStatEffects(this);
            }
        }

        /** 
         * Called by Modifier. Removes mod from manager's list after all effects are removed. 
         **/
        public void RemoveModifierFromList(Modifier modifier)
        {
            ModifierWrapper existingWrapper = ExistingMod(modifier);
            if (existingWrapper == null) { return; }
            existingWrapper.DeactivateMod();
            modWrappers.Remove(existingWrapper);
            Debug.Log("Stat effect " + modifier.name + " removed from " + gameObject.name);
        }
        #endregion

        #region Class Helper Methods
        /** 
         * Checks if a ModifierWrapper with the same Mod already exists 
        **/
        private ModifierWrapper ExistingMod(Modifier modifier)
        {
            // Check each wrapper in the list
            foreach (ModifierWrapper wrapper in modWrappers)
            {
                // Compare if wrapper in the list has the same modifier (to be applied)
                if (modifier == wrapper.Mod)
                {
                    // Return existing wrapper
                    return wrapper;
                }
            }
            // Returns null if there is no existing wrapper with same modifier
            return null;
        }

        /** 
         * Wraps Mod in a Wrapper for internal management
        **/
        private ModifierWrapper WrapModifier(Modifier mod)
        {
            ModifierWrapper wrapper = new ModifierWrapper(this, mod, mod.Context.duration);
            return wrapper;
        }
        #endregion

        #region MODIFIER ACTIONS
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

        public void DealDamage(int amount)
        {
            characterStats.TakeDamage(amount);
        }

        public void DealPercentDamage(float amount)
        {
            characterStats.TakePercentageDamage(amount);
        }
        #endregion

        #region Cleanup
        private void OnDisable()
        {
            // Failsafe
            StopAllCoroutines();
            modWrappers.Clear();
        }
        #endregion

    }
}