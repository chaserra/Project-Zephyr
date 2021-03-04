using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Stats;

namespace Zephyr.Combat.Mods
{
    [CreateAssetMenu(fileName = "NewModifier", menuName = "Stat Modifier/Mod")]
    public class Modifier : ScriptableObject
    {
        public ModifierContext context;
        public ValidTargets targetSelector;
        public StatEffect[] statEffects;

        public void ApplyModifier(CharacterStats stats)
        {
            // TODO (Modifier): Make passing of properties generic
            for (int i = 0; i < statEffects.Length; i++)
            {
                statEffects[i].ApplyStatEffect(stats);
            }
        }

        [System.Serializable]
        public class ModifierContext
        {
            public bool isActive;
            public bool hasDuration;
            public float duration;

            public bool IsActive { get { return isActive; } }
        }

        public enum ValidTargets
        {
            PLAYER,
            ENEMY,
            OBJECT
        }
    }

}