﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "NewModifier", menuName = "Mods/Modifier")]
    public class Modifier : ScriptableObject
    {
        // Parameters
        [SerializeField] private ModifierContext context;
        [SerializeField] private ValidTargets target;
        [SerializeField] private StatEffect[] statEffects;

        // State
        private ModifierManager modManager;

        #region Properties
        public ModifierManager ModManager { get { return modManager; } }
        public ModifierContext Context { get { return context; } }
        public ValidTargets Target { get { return target; } }
        public StatEffect[] StatEffects { get { return statEffects; } }
        #endregion

        public void InitializeModifier(ModifierManager modifierManager)
        {
            if (modManager == null)
            {
                modManager = modifierManager;
            }
            ApplyStatEffects();
        }

        private void ApplyStatEffects()
        {
            for (int i = 0; i < statEffects.Length; i++)
            {
                statEffects[i].ApplyEffect(this);
            }
            modManager.StartCoroutine(StartModDuration());
        }

        private void Tick()
        {
            // Do DoT stuff
        }

        public void RemoveStatEffects()
        {
            for (int i = 0; i < statEffects.Length; i++)
            {
                statEffects[i].RemoveEffect(this);
            }
            modManager.RemoveModifierFromList(this);
        }

        IEnumerator StartModDuration()
        {
            if (!context.hasDuration) { yield break; }
            // TODO (Mods): Check if stackable. If not, reset duration.
            while (context.duration > 0)
            {
                context.duration -= Time.deltaTime;
                yield return null;
            }
            RemoveStatEffects();
        }

        [System.Serializable]
        public class ModifierContext
        {
            public bool isActive;
            public bool hasDuration;
            public float duration;
            public bool isStackable;
            //public int maxStacks;
            //public float chanceToApplyMod; // Design: Not sure if chance should be per mod or per skill            
        }

        public enum ValidTargets
        {
            SELF,
            TARGET
        }
    }
}