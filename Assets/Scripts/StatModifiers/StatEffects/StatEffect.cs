using UnityEngine;
using Zephyr.Stats;

namespace Zephyr.Combat.Mods
{
    public abstract class StatEffect : ScriptableObject, IStatEffect
    {
        public abstract void ApplyStatEffect(CharacterStats stats);
    }
}