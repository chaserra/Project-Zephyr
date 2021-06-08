using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Combat;
using Zephyr.Stats;
using Zephyr.UI;
using Zephyr.Util;

namespace Zephyr.Mods
{
    [RequireComponent(typeof(CharacterStats))]
    public class ModifierManager : MonoBehaviour
    {
        // Cache
        private CharacterStats characterStats;
        [SerializeField] private AilmentsList ailmentsList_Template;
        private AilmentsList ailmentsList;
        private UIEventListener eventListener;

        // State
        /** Keeps track of all active mods (wrapped to prevent SO overwriting) **/
        private List<ModifierWrapper> modWrappers = new List<ModifierWrapper>();
        
        // Properties
        public List<ModifierWrapper> ActiveMods { get { return modWrappers; } }
        public AilmentsList AilmentsList { get { return ailmentsList; } }

        private void Awake()
        {
            characterStats = GetComponent<CharacterStats>();
            if(TryGetComponent<UIEventListener>(out var e)) { eventListener = e; }
            if (ailmentsList_Template == null) { Debug.LogWarning("Missing Ailments List for " + gameObject.name); return; }
            ailmentsList = Instantiate(ailmentsList_Template); // Creates instance of SO
            ailmentsList.Initialize(this);
        }

        private void Update()
        {
            /* Tick Methods */
            // ModWrappers
            if (modWrappers.Count > 0)
            {
                foreach (ModifierWrapper wrapper in modWrappers)
                {
                    wrapper.Mod.Tick(this);
                }
            }
            // Ailments
            if (ailmentsList != null)
            {
                ailmentsList.Tick();
            }
        }

        #region MODIFIER MANAGEMENT
        public void AddModifier(Modifier modifier)
        {
            // Roll for proc
            if (!UtilityHelper.RollForProc(modifier.Context.procChance)) { return; }

            // If modifier is instant (no duration)
            if (!modifier.Context.hasDuration) 
            { 
                // Apply instants then remove the modifier
                modifier.InitializeModifier(this);
                modifier.RemoveStatEffects(this);
                return; 
            }

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
        }

        /**
         * Removes Buffs, Debuffs, or Ailment (Type-specific)
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
         * Remove target modifier. Used for specific mod type removals.
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
         * Used for when a modifier's duration ends.
         **/
        public void RemoveModifierFromList(Modifier modifier)
        {
            ModifierWrapper existingWrapper = ExistingMod(modifier);
            if (existingWrapper == null) { return; }
            existingWrapper.DeactivateMod();
            modWrappers.Remove(existingWrapper);
            RecheckAllAilmentStatus();
        }

        /**
         * Called after removing a Mod from the list. 
         * Ensures ongoing lower-level ailments activate if higher ailments' effects have ended / removed
         **/
        public void RecheckAllAilmentStatus()
        {
            for (int i = modWrappers.Count - 1; i >= 0; i--)
            {
                modWrappers[i].ReapplyAilments();
            }
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
        /* *******************
         * Modify Stat Effects 
         * *******************/
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

        /* ***************
         * Ailment Effects 
         * ***************/
        // Used to get if an ailment is already active
        public bool AilmentActive(Ailment ailmentToFind)
        {
            return ailmentsList.AilmentActive(ailmentToFind);
        }

        public void DealDamage(Attack attack)
        {
            var attackables = gameObject.GetComponentsInChildren<IAttackable>();

            foreach (IAttackable a in attackables)
            {
                a.OnAttacked(gameObject, attack);
            }
        }

        /* *******
         * Healing 
         * ********/
        public void DealHealing(Attack attack)
        {
            // Convert to negative damage value (heal) then pass to DealDamage
            var newAttack = new Attack(attack.Damage * -1);
            DealDamage(newAttack);
        }

        // Used for effects / ailments that do percentage damage/healing
        public int GetHealthPercentage(float amount)
        {
            return characterStats.GetHealthPercentValue(amount);
        }

        /* ***********
         * Stun Effect 
         * ************/
        public void Stun(bool isStunned)
        {
            // Toggle Stun if combatant
            ICombatant combatant = GetComponentInParent<ICombatant>();
            if (combatant == null) { return; }
            combatant.Stunned(isStunned);
        }
        #endregion

        #region Event Triggering
        /* ******************** 
         * UI Stat Effect Icons 
         * ********************/
        public void InvokeStatEffectUIEvent(UIStatEffect_SO statEffectImage, bool arg)
        {
            if (eventListener == null) { return; } // Only called if object listens to UI events
            eventListener.RaiseUIEvent(statEffectImage, arg);
        }
        #endregion

        #region Cleanup
        // Not used. Kept in just in case
        private void OnDisable()
        {
            // Failsafe
            StopAllCoroutines();
            modWrappers.Clear();
        }
        #endregion

    }
}