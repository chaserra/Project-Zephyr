using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "NewModifier", menuName = "Mods/Modifier")]
    public class Modifier : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperNotes;
        [Space]
#endif
        // Parameters
        [SerializeField] private string modName;
        [SerializeField] private ModifierContext context;
        [SerializeField] private ValidTargets target;
        [SerializeField] private StatEffect[] statEffects;

        #region Properties
        public string ModifierName { get { return modName; } }
        public ModifierContext Context { get { return context; } }
        public ValidTargets Target { get { return target; } }
        public StatEffect[] StatEffects { get { return statEffects; } }
        #endregion

        public void InitializeModifier(ModifierManager modManager)
        {
            ApplyStatEffects(modManager);
        }

        public void ApplyStatEffects(ModifierManager modManager)
        {
            for (int i = 0; i < statEffects.Length; i++)
            {
                statEffects[i].ApplyEffect(modManager);
            }
        }

        public void Tick(ModifierManager modManager)
        {
            for (int i = 0; i < statEffects.Length; i++)
            {
                statEffects[i].Tick(modManager);
            }
        }

        public void RemoveStatEffects(ModifierManager modManager)
        {
            for (int i = 0; i < statEffects.Length; i++)
            {
                statEffects[i].RemoveEffect(modManager);
            }
            modManager.RemoveModifierFromList(this);
        }

        public bool ProcModifier()
        {
            bool applyMods = Random.value < context.procChance;
            return applyMods;
        }

    }
}