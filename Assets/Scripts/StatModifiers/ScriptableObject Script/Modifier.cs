using System.Collections;
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
        public ModifierContext Context { get { return context; } }
        public ValidTargets Target { get { return target; } }
        public StatEffect[] StatEffects { get { return statEffects; } }
        #endregion

        public void InitializeModifier(ModifierManager modifierManager)
        {
            modManager = modifierManager;
            ApplyStatEffects();
        }

        private void ApplyStatEffects()
        {
            for (int i = 0; i < statEffects.Length; i++)
            {
                if (statEffects[i] is StatEffect_ModifyStats)
                {
                    AggregateEffectValues(statEffects[i], false);
                }
                statEffects[i].ApplyEffect();
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
                if (statEffects[i] is StatEffect_ModifyStats)
                {
                    AggregateEffectValues(statEffects[i], true);
                }
                statEffects[i].RemoveEffect();
            }
            modManager.RemoveModifierFromList(this);
        }

        private void AggregateEffectValues(StatEffect effect, bool reverseValues)
        {
            // Cast StatEffect to ModifyStats
            StatEffect_ModifyStats statMod = (StatEffect_ModifyStats)effect;

            // Initialize variables to pass to Mod Manager
            StatList targetStat = statMod.targetStat;
            float statModValue = statMod.modifierValue;
            bool statModIsPercent = statMod.isPercentage;

            modManager.AggregateStatValues(targetStat, statModValue, statModIsPercent, reverseValues);
        }

        IEnumerator StartModDuration()
        {
            if (!context.hasDuration) { yield break; }
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
        }

        public enum ValidTargets
        {
            SELF,
            TARGET
        }
    }
}