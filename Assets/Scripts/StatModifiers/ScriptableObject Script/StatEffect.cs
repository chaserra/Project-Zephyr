using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods
{
    public abstract class StatEffect : ScriptableObject, IStatEffect
    {
        public StatList targetStat;
        public abstract void ApplyEffect();
    }
}