using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "NewModifier", menuName = "Mods/Modifier")]
    public class Modifier : ScriptableObject
    {
        public ModifierContext context;
        public ValidTargets target;
        public List<StatEffect> statEffects;
        
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

        public List<StatEffect> ApplyStatEffects()
        {
            return statEffects;
        }
    }
}