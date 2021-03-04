using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.Stats;

namespace Zephyr.Combat.Mods
{
    [CreateAssetMenu(fileName = "NewBuff", menuName = "Modifiers/Buff")]
    public class Modifier : ScriptableObject
    {
        public float multiplier;
        public float duration;

        public void ApplyModifier(CharacterStats stats)
        {
            // Do stuff here
            // Preferrably generic modifications to stats
        }
    }

}