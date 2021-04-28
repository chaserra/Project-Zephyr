using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods
{
    public abstract class Ailment : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperNotes;
        [Space]
#endif
        // Attributes
        protected ModifierManager modManager;
        public string ailmentName;
        protected int currentAilmentLevel = 0;
        protected float tickInterval;
        protected float tickTimer = 0f;
        [SerializeField] protected Color textColor = Color.red;

        // State
        protected bool isActive = false;

        // Properties
        public bool IsActive { get { return isActive; } }

        public abstract void InitializeAilment(ModifierManager modifierManager, StatEffect statEffect);
        public abstract void RemoveAilment(ModifierManager modifierManager, StatEffect statEffect);
        public abstract void Tick(ModifierManager modifierManager);

        /**
         * Used to compare new ailment level to currently active ailment
         * Returns false (does not proceed with activation) if new ailment is lower level
         * But the lower level ailment is still active in the background and will activate once the
         *      higher level ailment has ended
         **/
        protected bool CheckAilmentStatus<T>(StatEffect statEffect, out T type) where T : StatEffect_Ailment
        {
            var effect = (T)statEffect;
            type = effect;
            if (effect == null) { return false; }
            if (effect.ailmentLevel < currentAilmentLevel) { return false; }
            return true;
        }

        /**
         * Reset all parent variables upon ailment deactivation
         * - Specific variables' reset within ailments are handled inside the specific ailment
         **/
        protected void ResetBaseAilmentValues()
        {
            isActive = false;
            currentAilmentLevel = 0;
            tickInterval = 0f;
            tickTimer = 0f;
        }
    }
}