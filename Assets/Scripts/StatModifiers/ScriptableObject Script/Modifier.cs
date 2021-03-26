using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "NewModifier", menuName = "Mods/Modifier")]
    public class Modifier : ScriptableObject
    {
        // Parameters
        [SerializeField] private string modName;
        [SerializeField] private ModifierContext context;
        [SerializeField] private ValidTargets target;
        [SerializeField] private StatEffect[] statEffects;

        // State
        private ModifierManager modManager;

        #region Properties
        public string ModifierName { get { return modName; } }
        public ModifierManager ModManager { get { return modManager; } }
        public ModifierContext Context { get { return context; } }
        public ValidTargets Target { get { return target; } }
        public StatEffect[] StatEffects { get { return statEffects; } }
        #endregion

        public void InitializeModifier(ModifierManager modifierManager)
        {
            modManager = modifierManager;
            ApplyStatEffects();
        }

        public void ApplyStatEffects()
        {
            for (int i = 0; i < statEffects.Length; i++)
            {
                statEffects[i].ApplyEffect(this);
            }
        }

        public void Tick()
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
    }
}