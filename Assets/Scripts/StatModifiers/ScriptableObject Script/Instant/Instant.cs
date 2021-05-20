using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods
{
    public abstract class Instant : ScriptableObject
    {
        public ModType targetModType;

        public abstract void CastInstant(ModifierManager modifierManager);
    }
}