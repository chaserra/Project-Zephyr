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
        [SerializeField] private List<ModifierWrapper> modWrappers = new List<ModifierWrapper>(); // TODO (cleanup): Remove serializefield

        private void Awake()
        {
            characterStats = GetComponent<CharacterStats>();
        }

        #region MODIFIER MANAGEMENT
        public void AddModifier(Modifier modifier)
        {
            ModifierWrapper existingWrapper = ExistingMod(modifier);

            if (existingWrapper != null) // If mod exists
            {
                // Check if stackable
                if (existingWrapper.Mod.Context.isStackable) 
                {
                    // Reset duration
                    existingWrapper.Duration = modifier.Context.duration;
                    // Stack buff to the list
                    existingWrapper.ReapplyModifiers();
                } 
                else
                {
                    // Reset duration
                    existingWrapper.Duration = modifier.Context.duration;
                }
            }
            else // If mod is new
            {
                ModifierWrapper newWrapper = WrapModifier(modifier);
                modWrappers.Add(newWrapper);
                newWrapper.InitializeWrapper();
            }
        }

        /* So far this is only used for debug */
        /* This specifically targets the RemoveStatEffects method. Might need to delete this method */
        public void RemoveModifier(Modifier modifier)
        {
            ModifierWrapper existingWrapper = ExistingMod(modifier);
            if (existingWrapper == null) { return; }
            existingWrapper.DeactivateMod();
            existingWrapper.Mod.RemoveStatEffects();
        }

        public void RemoveModifierFromList(Modifier modifier)
        {
            ModifierWrapper existingWrapper = ExistingMod(modifier);
            if (existingWrapper == null) { return; }
            existingWrapper.DeactivateMod();
            modWrappers.Remove(existingWrapper);
        }

        // TODO (Modifier Manager): Create remove buffs and remove debuffs methods
        // Iterate through list of wrappers and check buff type
        // Pass those wrappers to RemoveModifierFromList
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

        #region Cleanup
        private void OnDisable()
        {
            // Failsafe
            StopAllCoroutines();
            modWrappers.Clear();
        }
        #endregion

        #region ModifierWrapper SUBCLASS
        /**
        * Modifier Wrappers act as containers for Modifier scriptable objects
        * This makes it possible to separately keep track of instances of modifiers
        * Separate instance = easily referenced
        * Can keep track of their own state variables such as durations and number of stacks
        **/
        protected class ModifierWrapper
        {
            private ModifierManager modMgr;
            private Modifier mod;
            private float duration;
            private int currentStacks;
            private bool isActive = false;

            // Properties
            public Modifier Mod { get { return mod; } }
            public float Duration { get { return duration; } set { duration = value; } }
            public int CurrentStacks { get { return currentStacks; } }

            // Constructor
            public ModifierWrapper(ModifierManager manager, Modifier modifier, float duration)
            {
                modMgr = manager;
                mod = modifier;
                this.duration = duration;
            }

            /**
             * Initialize mod effects and start duration timer
             **/
            public void InitializeWrapper()
            {
                isActive = true;
                mod.InitializeModifier(modMgr);
                modMgr.StartCoroutine(StartModDuration());
                currentStacks++;
            }

            /**
             * Stack mod effects
             **/
            public void ReapplyModifiers()
            {
                if (currentStacks >= mod.Context.maxStacks) { return; }
                mod.ApplyStatEffects();
                currentStacks++;
            }

            /**
             * Duration countdown. Only starts if mod is initialized (new mod)
             * Called only when initialized to prevent Coroutine effect stacking (faster timer bug)
             * Counter is reset outside this coroutine via duration variable change
             **/
            IEnumerator StartModDuration()
            {
                if (!mod.Context.hasDuration) { yield break; }
                while (duration > 0 && isActive)
                {
                    duration -= Time.deltaTime;
                    yield return null;
                }
                if (!isActive) { yield break; } // Prevents negative stacking bug.
                for (int i = 0; i < currentStacks; i++)
                {
                    mod.RemoveStatEffects();
                }
                isActive = false;
            }

            public void DeactivateMod()
            {
                isActive = false;
            }

        }
        #endregion

    }
}