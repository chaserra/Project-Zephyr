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
            if (context.isActive) { return; }
            context.isActive = true;
            for (int i = 0; i < statEffects.Length; i++)
            {
                statEffects[i].ApplyEffect(modManager);
            }
            modManager.StartCoroutine(StartModDuration());
        }

        private void Tick()
        {
            if (context.isActive)
            {
                for (int i = 0; i < statEffects.Length; i++)
                {
                    statEffects[i].Tick(modManager);
                }
            }
        }

        public void RemoveStatEffects()
        {
            for (int i = 0; i < statEffects.Length; i++)
            {
                statEffects[i].RemoveEffect(modManager);
            }
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