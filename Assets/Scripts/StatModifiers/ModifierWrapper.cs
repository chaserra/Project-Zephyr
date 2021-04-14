using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods {
    /**
    * Modifier Wrappers act as containers for Modifier scriptable objects
    * This makes it possible to separately keep track of instances of modifiers
    * Separate instance = easily referenced
    * Can keep track of their own state variables such as durations and number of stacks
    **/
    public class ModifierWrapper
    {
        private ModifierManager modMgr;
        private Modifier mod;
        private float duration;
        private int currentStacks;
        private bool isActive = false;

        // Properties
        public Modifier Mod { get { return mod; } }
        public float Duration { get { return duration; } }
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
            TriggerUIEvent();
        }

        /**
         * Stack mod effects
         **/
        public void ReapplyModifiers()
        {
            if (currentStacks >= mod.Context.maxStacks) { return; }
            mod.ApplyStatEffects(modMgr);
            currentStacks++;
            TriggerUIEvent();
        }

        public void ResetModDuration()
        {
            duration = mod.Context.duration;
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
                // Remove effect stacks
                mod.RemoveStatEffects(modMgr);
            }
            isActive = false;
        }

        public void DeactivateMod()
        {
            isActive = false;
            TriggerUIEvent();
        }

        private void TriggerUIEvent()
        {
            for (int i = 0; i < mod.StatEffects.Length; i++)
            {
                if (mod.StatEffects[i].effectImage == null) { return; }
                modMgr.InvokeStatEffectUIEvent(mod.StatEffects[i].effectImage, isActive);
            }
        }

    }
}