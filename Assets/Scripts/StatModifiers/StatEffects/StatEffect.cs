using UnityEngine;
using Zephyr.Stats;

namespace Zephyr.Combat.Mods
{
    public abstract class StatEffect : ScriptableObject, IStatEffect
    {
        public enum StatList { Health, Speed, Rotation };
        public StatList statToAffect;
        public abstract void ApplyStatEffect(CharacterStats stats);
    }
}